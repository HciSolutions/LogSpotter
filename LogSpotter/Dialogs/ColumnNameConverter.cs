using System.Collections.Generic;
using System.ComponentModel;

namespace HciSolutions.LogSpotter.Dialogs
{
    public class ColumnNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            // true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // true will limit to list. false will show the list, but allow free-form entry.
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Columns);
        }

        public static List<string> Columns { get; } = new List<string>();
    }
}
