using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class TrackingPackageStep
    {
        #region Properties

        /// <summary>
        /// Gets or sets the date of the passing of tracking
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the step status
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the step status information tracking
        /// </summary>
        public String Description { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            var value = new StringBuilder();

            value.Append("Date: ");
            value.Append(Date);
            value.Append(";");

            if (!String.IsNullOrEmpty(Name))
            {
                if (value.Length > 0)
                    value.Append(" ");

                value.Append("Name: ");
                value.Append(Name);
                value.Append(";");
            }

            if (!String.IsNullOrEmpty(Description))
            {
                if (value.Length > 0)
                    value.Append(" ");

                value.Append("Description: ");
                value.Append(Description);
                value.Append(";");
            }

            return value.ToString();
        }

        #endregion
    }
}
