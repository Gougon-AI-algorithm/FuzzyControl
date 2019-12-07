using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FuzzyControl
{
    class Fuzzy
    {
        private List<Term> _co2Level = new List<Term>();
        private List<Term> _difLevel = new List<Term>();
        private List<Term> _hpLevel = new List<Term>();
        private List<Term> _co2Cross = new List<Term>();
        private List<Term> _difCross = new List<Term>();
        private List<DetermineTerm> _determineTerm = new List<DetermineTerm>();
        private List<Term> _hpTerms = new List<Term>();
        private List<Intersaction> _intersactions = new List<Intersaction>();
        private Term _hpTerm = new Term();
        private double _curTmp;
        private double _co2;
        private double _dif;
        private double _hp;

        public Fuzzy()
        {
            SetFuzzyCo2();
            SetFuzzyDif();
            SetFuzzyHp();
        }

        // 取得溫度狀態
        private string GetTmpStatu()
        {
            if (_curTmp < 15)
                return "好冷喔";
            else if (15 <= _curTmp && _curTmp < 25)
                return "有點冷";
            else if (25 <= _curTmp && _curTmp < 30)
                return "適中";
            else if (30 <= _curTmp && _curTmp < 35)
                return "有點熱";
            else
                return "好熱喔";
        }

        public float UpdateTemperatures(float Temperature, float ExpectedTemperature, float Co2, ref string TemperatureStatu)
        {
            Refresh();
            _curTmp = Temperature;
            _co2 = Co2;
            _dif = ExpectedTemperature - Temperature;
            SetHp();
            TemperatureStatu = GetTmpStatu();
            return (float)_hp;
        }

        // 重置
        private void Refresh()
        {
            _co2Level.Clear();
            _difLevel.Clear();
            _hpLevel.Clear();
            SetFuzzyCo2();
            SetFuzzyDif();
            SetFuzzyHp();
            _co2Cross.Clear();
            _difCross.Clear();
            _determineTerm.Clear();
            _hpTerms.Clear();
            _intersactions.Clear();
            _hpTerm = new Term();
            _co2 = 0.0;
            _dif = 0.0;
            _hp = 0.0;
        }

        // 設定馬力
        private void SetHp()
        {
            // 將模糊單點會經過tmp、co2的term都存起來
            // 利用經過term<tmp, co2>的卡氏積挑選出對應的term
            // 用規則將term對應到c
            if (_dif <= 0)
                _hp = 0;
            else
            {
                FindCrossTerms(_co2Cross, _co2Level, _co2);
                FindCrossTerms(_difCross, _difLevel, _dif);
                DetermineCo2AndDifTerms();
                DetermineHpTerms();
                FixHpTerm();
                CalcHp(_hpTerm);
            }
        }

        // 計算馬力
        private void CalcHp(Term aimTerm)
        {
            double hp1 = 0.0;
            double hp2 = 0.0;
            foreach (Equation eq in aimTerm.Equations)
            {
                hp1 += eq.X2 / 3 * eq.MaxInterval * eq.MaxInterval * eq.MaxInterval + eq.X / 2 * eq.MaxInterval * eq.MaxInterval + eq.Constant * eq.MaxInterval - (eq.X2 / 3 * eq.MinInterval * eq.MinInterval * eq.MinInterval + eq.X / 2 * eq.MinInterval * eq.MinInterval + eq.Constant * eq.MinInterval);
                hp2 += eq.X2 / 4 * eq.MaxInterval * eq.MaxInterval * eq.MaxInterval * eq.MaxInterval + eq.X / 3 * eq.MaxInterval * eq.MaxInterval * eq.MaxInterval + eq.Constant / 2 * eq.MaxInterval * eq.MaxInterval - (eq.X2 / 4 * eq.MinInterval * eq.MinInterval * eq.MinInterval * eq.MinInterval + eq.X / 3 * eq.MinInterval * eq.MinInterval * eq.MinInterval + eq.Constant / 2 * eq.MinInterval * eq.MinInterval);
            }
            _hp = hp2 / hp1;
        }

        // 找出模糊單點會經過的terms
        private void FindCrossTerms(List<Term> crossTerm, List<Term> fuzzyTerms, double value)
        {
            foreach (Term term in fuzzyTerms)
            {
                if (term.IsInInterval(value))
                {
                    crossTerm.Add(term);
                }
            }
        }

        // 將每一組co2和dif都加入determin term並決定要用哪一個term
        private void DetermineCo2AndDifTerms()
        {
            foreach (Term co2Term in _co2Cross)
            {
                foreach (Term difTerm in _difCross)
                {
                    double co2y = co2Term.GetY(_co2);
                    double dify = difTerm.GetY(_dif);
                    Term term = (co2y < dify) ? co2Term : difTerm;
                    _determineTerm.Add(new DetermineTerm(co2Term, difTerm, term, (co2y < dify) ? _co2 : _dif));
                }
            }
        }

        // 決定要用hp的哪一個term
        private void DetermineHpTerms()
        {
            foreach (DetermineTerm dTerm in _determineTerm)
            {
                Term hpLevel = InferenceHpLevel(dTerm.Co2Term, dTerm.DifTerm);
                _hpTerms.Add(hpLevel);
            }
            CleanRepeatHpTerms();
        }

        // 清除重複的hp terms
        private void CleanRepeatHpTerms()
        {
            for (int i = 0; i < _hpTerms.Count; i++)
            {
                for (int j = _hpTerms.Count - 1; j > i; j--)
                {
                    if (_hpTerms[i].IsSimilar(_hpTerms[j]))
                        _hpTerms.RemoveAt(j);
                }
            }
        }

        // 決定要用hp的哪一個term(rule)
        private Term InferenceHpLevel(Term co2Level, Term difLevel)
        {
            if (_co2Level[0] == co2Level && _difLevel[0] == difLevel)
                return _hpLevel[0];
            else if (_co2Level[0] == co2Level && _difLevel[1] == difLevel)
                return _hpLevel[1];
            else if (_co2Level[0] == co2Level && _difLevel[2] == difLevel)
                return _hpLevel[2];
            else if (_co2Level[1] == co2Level && _difLevel[0] == difLevel)
                return _hpLevel[0];
            else if (_co2Level[1] == co2Level && _difLevel[1] == difLevel)
                return _hpLevel[1];
            else if (_co2Level[1] == co2Level && _difLevel[2] == difLevel)
                return _hpLevel[2];
            else if (_co2Level[2] == co2Level && _difLevel[0] == difLevel)
                return _hpLevel[0];
            else if (_co2Level[2] == co2Level && _difLevel[1] == difLevel)
                return _hpLevel[0];
            else
                return _hpLevel[1];
        }

        // 將hp term修成正確的term
        private void FixHpTerm()
        {
            FixHpTermsY();
            CompositeTerms();
        }

        // 將所有hp terms的y修正
        private void FixHpTermsY()
        {
            // 將三角形切成梯形
            for (int i = 0; i < _hpTerms.Count; i++)
            {
                double y = _determineTerm[i].GetY();
                _hpTerms[i].ReShape(y);
            }

            // 刪除跟x軸重疊的方程式
            for (int i = _hpTerms.Count - 1; i >= 0; i--)
            {
                if (_determineTerm[i].GetY() == 0)
                    _hpTerms.RemoveAt(i);
            }
        }

        // 將hp terms全部結合在一起
        private void CompositeTerms()
        {
            FindIntersactions();
            CleanIntersactions();
            CleanHpTerms();
            CreateHpTerm();
        }

        // 創造hp term
        private void CreateHpTerm()
        {
            foreach (Term term in _hpTerms)
            {
                foreach (Equation eq in term.Equations)
                {
                    AddEquationToHpTerm(eq, term);
                }
            }
            _hpTerm.RemovePoint();
        }

        // 把equation加入hp term
        private void AddEquationToHpTerm(Equation eq, Term term)
        {
            foreach (Intersaction intersaction in _intersactions)
            {
                if (eq.IsInInterval(intersaction.X))
                {
                    DetermineEquationToHpTerm(eq, intersaction);
                    return;
                }
            }
            foreach (Term hpTerm in _hpTerms)
            {
                if (hpTerm.IsInInterval(eq.MinInterval) && hpTerm.IsInInterval(eq.MaxInterval) && !term.IsSimilar(hpTerm))
                {
                    double eqMaxy = (eq.GetY(eq.MinInterval) > eq.GetY(eq.MaxInterval)) ? eq.GetY(eq.MinInterval) : eq.GetY(eq.MaxInterval);
                    double termMaxy = 0;
                    foreach (Equation termEq in hpTerm.Equations)
                    {
                        double maxy = (termEq.GetY(termEq.MinInterval) > termEq.GetY(termEq.MaxInterval)) ? termEq.GetY(termEq.MinInterval) : termEq.GetY(termEq.MaxInterval);
                        termMaxy = (termMaxy > maxy) ? termMaxy : maxy;
                    }
                    if (eqMaxy < termMaxy)
                        return;
                }
            }
            _hpTerm.AddEquation(eq);
        }

        // 將不同interval的方程式加到hp term中
        private void DetermineEquationToHpTerm(Equation eq, Intersaction intersaction)
        {
            if (eq.X != 0)
            {
                if (eq.X > 0)
                    _hpTerm.AddEquation(new Equation(eq.Constant, eq.X, eq.X2, intersaction.X, eq.MaxInterval));
                else
                    _hpTerm.AddEquation(new Equation(eq.Constant, eq.X, eq.X2, eq.MinInterval, intersaction.X));
            }
            else
            {
                Equation parallel = (intersaction.Equation1.X == 0) ? intersaction.Equation1 : intersaction.Equation2;
                Equation slash = (intersaction.Equation1.X != 0) ? intersaction.Equation1 : intersaction.Equation2;
                if (slash.X > 0)
                    parallel.MaxInterval = intersaction.X;
                else
                    parallel.MinInterval = intersaction.X;
                _hpTerm.AddEquation(parallel);
            }
        }

        // 將不要的term去除
        private void CleanHpTerms()
        {
            for (int i = 0; i < _hpTerms.Count; i++)
            {
                for (int j = _hpTerms.Count - 1; j > i; j--)
                {
                    if (_hpTerms[i].IsSimilar(_hpTerms[j]))
                    {
                        RemoveSmallOne(i, j);
                    }
                }
            }
        }

        // 將比較小的term刪掉
        private void RemoveSmallOne(int lhs, int rhs)
        {
            for (int i = 0; i < _hpTerms[lhs].Equations.Count; i++)
            {
                if (_hpTerms[lhs].Equations[i].IsParallelDown(_hpTerms[rhs].Equations[i]) == 1)
                    Swap(lhs, rhs);
                else if (_hpTerms[lhs].Equations[i].IsParallelDown(_hpTerms[rhs].Equations[i]) == -1)
                    break;
            }
            _hpTerms.Remove(_hpTerms[rhs]);
        }

        // 將2個馬力交換
        private void Swap(int lhs, int rhs)
        {
            Term temp = _hpTerms[lhs];
            _hpTerms[lhs] = _hpTerms[rhs];
            _hpTerms[rhs] = temp;
        }

        // 將不要的交點去除
        private void CleanIntersactions()
        {
            for (int i = _intersactions.Count - 1; i >= 0; i--)
            {
                foreach (Term term in _hpTerms)
                {
                    if (CleanIntersaction(_intersactions[i], term))
                        break;
                }
            }
        }

        // 若交點y不是最大或跟其他交點相同的則清除它
        private bool CleanIntersaction(Intersaction intersaction, Term term)
        {
            if (!term.IsInInterval(intersaction.X))
                return false;
            foreach (Equation eq in term.Equations)
            {
                if (eq.IsInInterval(intersaction.X) && eq.GetY(intersaction.X) > intersaction.Y)
                {
                    _intersactions.Remove(intersaction);
                    return true;
                }
            }
            return false;
        }

        // 找到所有交點
        private void FindIntersactions()
        {
            foreach (Term term in _hpTerms)
            {
                foreach (Equation eq in term.Equations)
                {
                    FindIntersactionsFromEquation(eq, term);
                }
            }
        }

        // 找到term的交點
        private void FindIntersactionsFromEquation(Equation equation, Term term)
        {
            foreach (Term hpTerm in _hpTerms)
            {
                if (term.IsSimilar(hpTerm))
                    continue;
                foreach (Equation eq in hpTerm.Equations)
                {
                    AddIntersaction(equation, eq);
                }
            }
        }

        // 判斷是否重疊並加入交點
        private void AddIntersaction(Equation lhs, Equation rhs)
        {
            if (lhs.IsEquationsIntervalOverlap(rhs) && !lhs.IsEqual(rhs))
            {
                // 1     y = ax + b
                // 2     y = cx + d
                // 1-2   -(b - d) / (a - c)
                double x = -(lhs.Constant - rhs.Constant) / (lhs.X - rhs.X);
                double y = lhs.X * x + lhs.Constant;
                Intersaction newInter = new Intersaction(x, y, lhs, rhs);
                if (lhs.MinInterval <= x && x <= lhs.MaxInterval &&
                    rhs.MinInterval <= x && x <= rhs.MaxInterval && 
                    !_intersactions.Any(eq => eq.IsEqual(newInter)))
                    _intersactions.Add(newInter);
            }
        }

        // 設定模糊溫室氣體
        private void SetFuzzyCo2()
        {
            // 低濃度(0 ~ 50)
            _co2Level.Add(new Term());
            _co2Level.Last().AddEquation(new Equation(1, 0, 25));
            _co2Level.Last().AddEquation(new Equation(2, -0.04, 25, 50));

            // 中濃度(25 ~ 75)
            _co2Level.Add(new Term());
            _co2Level.Last().AddEquation(new Equation(-1, 0.04, 25, 50));
            _co2Level.Last().AddEquation(new Equation(3, -0.04, 50, 75));

            // 高濃度(50 ~ 100)
            _co2Level.Add(new Term());
            _co2Level.Last().AddEquation(new Equation(-2, 0.04, 50, 75));
            _co2Level.Last().AddEquation(new Equation(1, 75, 100.1));
        }

        // 設定模糊溫差
        private void SetFuzzyDif()
        {
            // 低溫差(0 ~ 15)
            _difLevel.Add(new Term());
            _difLevel.Last().AddEquation(new Equation(1, 0, 10));
            _difLevel.Last().AddEquation(new Equation(3, -0.2, 10, 15));

            // 中溫差(5 ~ 25)
            _difLevel.Add(new Term());
            _difLevel.Last().AddEquation(new Equation(-0.5, 0.1, 5, 15));
            _difLevel.Last().AddEquation(new Equation(2.5, -0.1, 15, 25));

            // 高溫差(15 ~ 30)
            _difLevel.Add(new Term());
            _difLevel.Last().AddEquation(new Equation(-3, 0.2, 15, 25));
            _difLevel.Last().AddEquation(new Equation(1, 25, 10000));
        }

        // 設定模糊馬力
        private void SetFuzzyHp()
        {
            // 低馬力(0 ~ 50)
            _hpLevel.Add(new Term());
            _hpLevel.Last().AddEquation(new Equation(1, 0, 25));
            _hpLevel.Last().AddEquation(new Equation(2, -0.04, 25, 50));

            // 中馬力(25 ~ 75)
            _hpLevel.Add(new Term());
            _hpLevel.Last().AddEquation(new Equation(-1, 0.04, 25, 50));
            _hpLevel.Last().AddEquation(new Equation(3, -0.04, 50, 75));

            // 高馬力(50 ~ 100)
            _hpLevel.Add(new Term());
            _hpLevel.Last().AddEquation(new Equation(-2, 0.04, 50, 75));
            _hpLevel.Last().AddEquation(new Equation(1, 75, 100));
        }
    }
}
