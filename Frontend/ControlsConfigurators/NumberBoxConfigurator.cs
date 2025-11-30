using Microsoft.UI.Xaml.Controls;
using Windows.Globalization.NumberFormatting;

namespace Frontend.ControlsConfigurators;

public class NumberBoxConfigurator<T> : IControlConfigurator<T>
    where T : NumberBox
{
    public void ConfigureControl(T control)
    {
        var rounder = new IncrementNumberRounder();
        rounder.Increment = 0.01;
        rounder.RoundingAlgorithm = RoundingAlgorithm.RoundHalfUp;

        var formatter = new DecimalFormatter();
        formatter.IntegerDigits = 1;
        formatter.FractionDigits = 2;
        formatter.NumberRounder = rounder;

        control.NumberFormatter = formatter;
    }
}
