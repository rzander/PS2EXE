using System;
using System.IO;

namespace PS2EXE
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_Compile_Click(object sender, EventArgs e)
        {
            string sFilename = Environment.ExpandEnvironmentVariables(Server.MapPath("~/App_Data/" + Path.GetRandomFileName().Split('.')[0] + ".exe"));
            //string sResname = Environment.ExpandEnvironmentVariables(Server.MapPath("~/App_Data/" + Path.GetRandomFileName().Split('.')[0]));
            //Directory.CreateDirectory(sResname);


            //string sFilename = Environment.ExpandEnvironmentVariables(Server.MapPath("~/App_Data/" + tb_Filename.Text));
            string sResname = Environment.ExpandEnvironmentVariables(Server.MapPath("~/App_Data/Resources.resx"));
            try
            {
                File.Delete(sFilename);
                File.Delete(sResname);
            }
            catch { }

            CreateExe oExe = new CreateExe(sFilename, Server.MapPath("~/Bin"));
            oExe.cp.CompilerOptions = Environment.ExpandEnvironmentVariables("/win32icon:\"" + Server.MapPath("~/Images/powershell.ico") +"\" /optimize");

            /*if (imgIcon.Tag != null)
                oExe.Icon = imgIcon.Tag as byte[];*/

            //oExe.Sources.Add(tbPSCode.Text);
            oExe.Sources.Add(Properties.Resources.Source);
            oExe.Sources.Add(Properties.Resources.Assembly);

            System.Resources.ResourceWriter writer = new System.Resources.ResourceWriter(sResname);
            writer.AddResource("psCode.ps1", " " + tb_PSSCript.Text.Trim());
            writer.Generate();
            writer.Close();
            oExe.cp.EmbeddedResources.Add(sResname);

            if (!oExe.Compile())
            {
                oExe.ToString();
                //MessageBox.Show("Failed to create .Exe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //File.Delete(sResname);
            //Directory.Delete(sResname);

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + tb_Filename.Text);
            Response.TransmitFile(sFilename);
            Response.End();

            /*
            try
            {
                File.Delete(sFilename);
                File.Delete(sResname);
            }
            catch { } */
        }
    }
}