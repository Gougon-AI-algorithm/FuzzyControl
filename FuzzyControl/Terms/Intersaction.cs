using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyControl
{
    class Intersaction
    {
        private double _x;
        private double _y;
        Equation _equation1;
        Equation _equation2;

        public Intersaction()
        {
            _x = 0;
            _y = 0;
        }

        public Intersaction(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Intersaction(double x, double y, Equation eq1, Equation eq2)
        {
            _x = x;
            _y = y;
            _equation1 = eq1;
            _equation2 = eq2;
        }

        // 判斷是否一樣
        public bool IsEqual(Intersaction intersaction)
        {
            return _x == intersaction.X &&
                _y == intersaction.Y;
        }

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public Equation Equation1
        {
            get
            {
                return _equation1;
            }
            set
            {
                _equation1 = value;
            }
        }

        public Equation Equation2
        {
            get
            {
                return _equation2;
            }
            set
            {
                _equation2 = value;
            }
        }
    }
}
