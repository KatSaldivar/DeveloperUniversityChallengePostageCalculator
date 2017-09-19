using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengePostageCalculator
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void handleChange(object sender, EventArgs e)
        {
            //performCalculation();
            //do the values in the text boxes/check boxes exist?
            if (!valuesExist()) return;

            //Determine volume (flat is possible)
            int volume = 0;
            if (!tryGetVolume(out volume)) return;

            //what is the multiplier/ which radio was selected?
            double postageMultiplier = getPostageMultiplier();

            //determine cost
            double cost = volume * postageMultiplier;

            //show user
            resultLabel.Text = String.Format("Your parcel will cost {0:C} to ship.", cost);
        }
        
        private bool valuesExist()
        {
            if (!airRadioButton.Checked && !groundRadioButton.Checked && !nextDayRadioButton.Checked)
                return false;

            if (widthTextBox.Text.Trim().Length == 0 || heightTextBox.Text.Trim().Length == 0)
                return false;
            return true;
        }
        
        private bool tryGetVolume(out int volume)
        {
             volume = 0;
            int width = 0;
            int height = 0;
            int length = 0;
            if (!int.TryParse(widthTextBox.Text.Trim(), out width)) return false;
            if (!int.TryParse(heightTextBox.Text.Trim(), out height)) return false;
            if (!int.TryParse(lengthTextBox.Text.Trim(), out length)) length = 1;

            volume = width * height * length;
            return true;
        }

        private double getPostageMultiplier()
        {
            if (groundRadioButton.Checked) return .15;
            else if (airRadioButton.Checked) return .25;
            else if (nextDayRadioButton.Checked) return .45;
            else return 0;
        }
    }
}