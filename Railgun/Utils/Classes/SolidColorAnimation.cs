using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Railgun.Utils.Classes
{
    public class SolidColorAnimation : ColorAnimation
    {
        public SolidColorBrush ToBrush
        {
            get => To == null ? null : new SolidColorBrush(To.Value);
            set => To = value?.Color;
        }
    }
}