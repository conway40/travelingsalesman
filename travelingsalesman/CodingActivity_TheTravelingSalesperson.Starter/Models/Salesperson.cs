using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TheTravelingSalesperson
{
    public class Salesperson
    {
        #region FIELDS

        private string _firstName;

        private string _lastName;

        private string _accountID;

        private List<string> _citiesVisited;

        private WidgetItemStock _currentStock;


        #endregion

        #region PROPERTIES

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string AccountID
        {
            get { return _accountID; }
            set { _accountID = value; }
        }

        public List<string> CitiesVisited
        {
            get { return _citiesVisited; }
            set { _citiesVisited = value; }
        }

        public WidgetItemStock CurrentStock
        {
            get { return _currentStock; }
            set { _currentStock = value; }
        }

        #endregion


        #region CONSTRUCTORS

        public Salesperson()
        {
            _citiesVisited = new List<string>();
            _currentStock = new WidgetItemStock();
        }

        public Salesperson(string lastName, string firstName)
        {
            _lastName = lastName;
            _firstName = firstName;
            _accountID = AccountID;
            _citiesVisited = new List<string>();
            _currentStock = new WidgetItemStock();
        }
        

        #endregion


        #region METHODS

        
        #endregion
    }
}
