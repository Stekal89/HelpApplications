using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPF_ComboBoxBindingToEnum.Model
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Status
    {
        [Description("This is horrible")]
        Horrible,
        [Description("This is bad")]
        Bad,
        [Description("This is so so")]
        SoSo,
        [Description("This is good")]
        Good,
        [Description("This is better")]
        Better,
        [Description("This is best")]
        Best
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum CarBrand
    {
        [Description("4 Rings")]
        Audi,
        [Description("Big German")]
        BMW,
        [Description("Woertersee-Car")]
        Opel,
        [Description("Golf Series")]
        VW
    }
    public class TestClass
    {
        public Status Status { get; set; }
        public CarBrand CarBrand { get; set; }
    }
}
