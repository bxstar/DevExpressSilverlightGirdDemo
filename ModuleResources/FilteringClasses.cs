using System;
using System.Windows.Data;
using DevExpress.Data.Filtering;

namespace GridDemo
{
    public class DoubleToCriteriaOperatorConverter : IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BinaryOperator op = value as BinaryOperator;
            if (object.ReferenceEquals(op, null))
                return 0d;
            OperandValue operandValue = op.RightOperand as OperandValue;
            return operandValue.Value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new BinaryOperator("Quantity", Math.Round((double)value), BinaryOperatorType.Greater);
        }
        #endregion
    }
}
