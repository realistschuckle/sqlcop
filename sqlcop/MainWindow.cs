using System;
using System.IO;
using System.Windows.Forms;

namespace sqlcop
{
	public class MainWindow : Form
	{
		public MainWindow()
		{
			ConfigureMenu();
		}
		
		private void ConfigureMenu()
		{
			MainMenu menu = new MainMenu();
			MenuItem file = menu.MenuItems.Add("File");
			MenuItem open = file.MenuItems.Add("Open...", OpenFile);
			open.Shortcut = Shortcut.CtrlO;
			open.ShowShortcut = true;
			MenuItem exit = file.MenuItems.Add("Exit", (o, e) => Close());
			exit.Shortcut = Shortcut.CtrlQ;
			exit.ShowShortcut = true;
			Menu = menu;
		}
		
		private void OpenFile(object sender, EventArgs args)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Sql Files (*.sql)|All Files (*.*)";
			if(dialog.ShowDialog() == DialogResult.OK) {
				using(Stream input = File.OpenRead(dialog.FileName)) {
					Scanner scanner = new Scanner(input);
					Parser parser = new Parser(scanner);
					if(!parser.Parse()) {
						MessageBox.Show("That file does not contain well-formed T-SQL.",
					                "Bad SQL File",
					                MessageBoxButtons.OK,
					                MessageBoxIcon.Error);
						return;
					}
				}
			}
		}
	}
}

