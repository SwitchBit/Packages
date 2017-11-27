using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwitchBit.Xaml.Xamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NullContentTemplate : ViewCell
	{
		public NullContentTemplate ()
		{
			InitializeComponent ();
		}
	}
}