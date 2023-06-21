/*
* Copyright 2023 Plextora
* Licensed under the GPL-3.0 license (https://www.gnu.org/licenses/gpl-3.0.en.html#license-text)
*/

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