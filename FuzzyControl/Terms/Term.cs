using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace FuzzyControl
{
    class Term
    {
        private List<Equation> _equations = new List<Equation>();
        private double _minInterval;
        private double _maxInterval;

        public Term()
        {
            // empty body
        }

        // 加入方程式
        public void AddEquation(Equation equation)
        {
            _equations.Add(equation);
            double min = 1000000;
            double max = 0;
            foreach (Equation eq in _equations)
            {
                min = (min < eq.MinInterval) ? min : eq.MinInterval;
                max = (max > eq.MaxInterval) ? max : eq.MaxInterval;
            }
            _minInterval = min;
            _maxInterval = max;
        }

        // 判斷值是否在區間內
        public bool IsInInterval(double value)
        {
            foreach (Equation equation in _equations)
            {
                if (equation.IsInInterval(value))
                    return true;
            }
            return false;
        }

        // 判斷兩個term是否相似且區間也一樣
        public bool IsSimilar(Term term)
        {
            return _minInterval == term._minInterval && _maxInterval == term._maxInterval;
        }

        // 取得equations
        public List<Equation> Equations
        {
            get
            {
                return _equations;
            }
        }

        // 取得y
        public double GetY(double x)
        {
            foreach (Equation equation in _equations)
            {
                if (equation.IsInInterval(x))
                    return equation.GetY(x);
            }
            return 0.0;
        }

        // 依照給定的y切割函數圖形
        public void ReShape(double y)
        {
            List<Intersaction> intersactions = new List<Intersaction>();
            FindIntersactions(intersactions, y);
            CutShape(intersactions);
        }

        // 找到所有與y相交的交點並更改interval
        private void FindIntersactions(List<Intersaction> intersactions, double y)
        {
            foreach (Equation eq in _equations)
            {
                double x = eq.GetX(y);
                if (x < 0 || x == eq.MinInterval || x == eq.MaxInterval)
                    continue;
                if (eq.X > 0)
                    eq.MaxInterval = x;
                else
                    eq.MinInterval = x;
                intersactions.Add(new Intersaction(x, y));
            }
        }

        // 切割圖形(加入方程式)
        private void CutShape(List<Intersaction> intersactions)
        {
            if (intersactions.Count == 1)
                ParallelCut(intersactions);
            else if (intersactions.Count == 2)
                TriangleCut(intersactions);
            RemovePoint();
        }

        // 切割帶有平行線的圖形
        private void ParallelCut(List<Intersaction> intersactions)
        {
            for (int i = _equations.Count - 1; i >= 0; i--)
            {
                if (_equations[i].X > 0)
                {
                    _equations[i].MaxInterval = intersactions[0].X;
                    _equations.Add(new Equation(intersactions[0].Y, _equations[i].MaxInterval, _maxInterval));
                }
                else if (_equations[i].X < 0)
                {
                    _equations[i].MinInterval = intersactions[0].X;
                    _equations.Add(new Equation(intersactions[0].Y, _minInterval, _equations[i].MinInterval));
                }
                else
                    _equations.RemoveAt(i);
            }
        }

        // 切割三角形
        private void TriangleCut(List<Intersaction> intersactions)
        {
            foreach (Equation eq in _equations)
            {
                if (eq.IsInInterval(intersactions[0].X))
                    eq.MaxInterval = intersactions[0].X;
                else if (eq.IsInInterval(intersactions[1].X))
                    eq.MinInterval = intersactions[1].X;
            }
            _equations.Add(new Equation(intersactions[1].Y, intersactions[0].X, intersactions[1].X));
        }

        // 移除一點方程式
        public void RemovePoint()
        {
            for (int i = _equations.Count - 1; i >= 0; i--)
            {
                if (_equations[i].MinInterval == _equations[i].MaxInterval)
                    _equations.RemoveAt(i);
            }
        }

        // equations長度

        // 最小x

        // 最大x
    }
}