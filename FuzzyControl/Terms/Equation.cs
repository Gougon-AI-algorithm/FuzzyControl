using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyControl
{
    class Equation
    {
        private double _constant;
        private double _x;
        private double _x2;
        private double _minInterval;
        private double _maxInterval;

        public Equation()
        {
            SetWholeEquation(0, 0, 0, 0, 0);
        }

        public Equation(double constant, double minInterval, double maxInterval)
        {
            SetWholeEquation(constant, 0, 0, minInterval, maxInterval);
        }

        public Equation(double constant, double x, double minInterval, double maxInterval)
        {
            SetWholeEquation(constant, x, 0, minInterval, maxInterval);
        }

        public Equation(double constant, double x, double x2, double minInterval, double maxInterval)
        {
            SetWholeEquation(constant, x, x2, minInterval, maxInterval);
        }

        public double MinInterval
        {
            get
            {
                return _minInterval;
            }
            set
            {
                _minInterval = value;
            }
        }

        public double MaxInterval
        {
            get
            {
                return _maxInterval;
            }
            set
            {
                _maxInterval = value;
            }
        }

        public double X
        {
            get
            {
                return _x;
            }
        }

        public double X2
        {
            get
            {
                return _x2;
            }
        }

        public double Constant
        {
            get
            {
                return _constant;
            }
        }

        // 判斷值是否在方程式區間
        public bool IsInInterval(double value)
        {
            return _minInterval <= value && value < _maxInterval;
        }

        // 判斷方程式是否一樣
        public bool IsEqual(Equation eq)
        {
            return eq._x2 == _x2 && eq._x == _x && eq._constant == _constant;
        }

        // 判斷有沒有在相同區間
        public bool IsEquationsIntervalOverlap(Equation eq)
        {
            return !(_minInterval > eq._maxInterval) && !(eq._minInterval > _maxInterval);
        }

        // 判斷使否為垂直向下移動的方程式
        public int IsParallelDown(Equation eq)
        {
            if (_x == eq._x && _x2 == eq._x2 && _constant > eq._constant)
                return -1;
            else if (_x == eq._x && _x2 == eq._x2 && _constant < eq._constant)
                return 1;
            return 0;
        }

        // 算y
        public double GetY(double x)
        {
            return _x2 * (x * x) + _x * x + _constant;
        }

        // 算x
        public double GetX(double y)
        {
            if (_x == 0)
                return -1;
            double x = (y - _constant) / _x;
            if (_minInterval <= x && x <= _maxInterval)
                return x;
            return -1;
        }

        // 設定1元2次方程式的參數
        private void SetWholeEquation(double constant, double x, double x2, double minInterval, double maxInterval)
        {
            _constant = constant;
            _x = x;
            _x2 = x2;
            _minInterval = minInterval;
            _maxInterval = maxInterval;
        }
    }
}
