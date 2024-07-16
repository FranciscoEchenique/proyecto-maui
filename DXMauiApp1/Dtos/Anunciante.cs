using DevExpress.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Dtos
{
    public class Anunciante
    {
        [DataFormNumericEditor(IsReadOnly = true)]
        public int IdAnunciante { get; set; }

        [DataFormTextEditor()]
        public string Nombre { get; set; }

        [DataFormSwitchEditor()]
        public bool Habilitado { get; set; }

        public int MaxNroOTAnunciante { get ; set; }

        [DataFormTextEditor(MaxCharacterCount = 100)]
        public string Interface1 { get; set; }

        [DataFormTextEditor(MaxCharacterCount = 100)]
        public string Interface2 { get; set; }
    }
}
