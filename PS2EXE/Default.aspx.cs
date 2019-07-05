using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web.UI;

namespace PS2EXE
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_Compile2_Click(object sender, EventArgs e)
        {
            string sPSTest = TestPowerShellSyntax(tb_PSSCript.Text);
            if (!string.IsNullOrEmpty(sPSTest))
            {
                ShowMessage("Syntax Error:" + sPSTest, MessageType.Error);
                return;
            }
        }
        protected void bt_Compile_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tb_Filename.Text))
            {
                ShowMessage("Filename is missing.. !", MessageType.Error);
                return;
            }
            if (string.IsNullOrEmpty(tb_PSSCript.Text))
            {
                ShowMessage("PowerShell script cannot be empty.. !", MessageType.Error);
                return;
            }
            string sPSTest = TestPowerShellSyntax(tb_PSSCript.Text);
            if (!string.IsNullOrEmpty(sPSTest))
            {
                ShowMessage("Syntax Error:" + sPSTest, MessageType.Error);
                return;
            }
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
            oExe.cp.CompilerOptions = Environment.ExpandEnvironmentVariables("/win32icon:\"" + Server.MapPath("~/Images/powershell.ico") + "\" /optimize");

            oExe.Sources.Add(Properties.Resources.Source);
            oExe.Sources.Add(Properties.Resources.Assembly);

            System.Resources.ResourceWriter writer = new System.Resources.ResourceWriter(sResname);
            writer.AddResource("psCode.ps1", " " + Zip(tb_PSSCript.Text.Trim()));
            writer.Generate();
            writer.Close();
            oExe.cp.EmbeddedResources.Add(sResname);

            if (!oExe.Compile())
            {
                //MessageBox.Show("Failed to create .Exe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                sFilename.ToString();
            }

            //File.Delete(sResname);
            //Directory.Delete(sResname);

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + tb_Filename.Text);
            Response.TransmitFile(sFilename);
            Response.End();


            try
            {
                File.Delete(sFilename);
                File.Delete(sResname);
            }
            catch { }
        }

        public enum MessageType { Success, Error, Info, Warning };

        protected void ShowMessage(string Message, MessageType type)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + Message + "');</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        /// <summary>
        /// Check if PS Code is valid
        /// </summary>
        /// <param name="sPSCode"></param>
        /// <returns>empty string = no error; first errormessage on error</returns>
        public string TestPowerShellSyntax(string sPSCode)
        {
            try
            {
                System.Management.Automation.Language.Token[] aTokens = null;
                System.Management.Automation.Language.ParseError[] aErrors = null;

                var pAST = System.Management.Automation.Language.Parser.ParseInput(sPSCode, out aTokens, out aErrors);
                if (aErrors.Length == 0)
                    return "";
                else
                    return aErrors[0].Message.ToString(); ;
            }
            catch
            {
                return "generic Error..";
            }


        }

        //Zip-Unzip Source from: http://stackoverflow.com/questions/7343465/compression-decompression-string-with-c-sharp

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static string Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return Convert.ToBase64String(mso.ToArray());
                //return mso.ToArray();
            }
        }

        public static string Unzip(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
    }
}