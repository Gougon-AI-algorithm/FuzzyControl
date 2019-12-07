using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyControl
{
    class DetermineTerm
    {
        private Term _co2Term = null;
        private Term _difTerm = null;
        private Term _aimTerm = null;
        double _value;

        public DetermineTerm()
        {
            // empty body
        }

        public DetermineTerm(Term co2Term, Term difTerm, Term aimTerm, double value)
        {
            _co2Term = co2Term;
            _difTerm = difTerm;
            _aimTerm = aimTerm;
            _value = value;
        }

        public Term Co2Term
        {
            get
            {
                return _co2Term;
            }
            set
            {
                _co2Term = value;
            }
        }

        public Term DifTerm
        {
            get
            {
                return _difTerm;
            }
            set
            {
                _difTerm = value;
            }
        }

        public Term AimTerm
        {
            get
            {
                return _aimTerm;
            }
            set
            {
                _aimTerm = value;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        // 取得y
        public double GetY()
        {
            return _aimTerm.GetY(_value);
        }
    }
}
