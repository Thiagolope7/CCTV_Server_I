using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CCTV_Server
{
    class Citilog
    {
        public static void mover_ptz(int id)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("10.158.64.53", 44000);
            }
            catch (SocketException)
            {
                string texto2 = "<CitiCommand Id=" + '\u0022' + "6" + '\u0022' + " Type=" + '\u0022' + "PtzControl" + '\u0022' + "><PtzControl CameraId=" + '\u0022' + id.ToString() + '\u0022' + " CameraName=" + '\u0022' + "ALL" + '\u0022' + " PanCommand=" + '\u0022' + "Right" + '\u0022' + "/></CitiCommand>";
                Preview.log.Info(texto2);
                //no se conecta con citilog
                //string  texto1 = @"<CitiCommand Id=""6"" Type=""PtzControl""><PtzControl CameraId=""";
                // texto1 += id.ToString();
                // texto1 += @"""CameraName = ""ALL"" PanCommand = ""Right"" /></ CitiCommand >";

                return;
            }
            NetworkStream networkStream = tcpClient.GetStream();
            string texto, conf, mover;
            while (true)
            {
                if (networkStream.CanWrite && networkStream.CanRead)
                {


                    texto = "<CitiCommand Id=" + '\u0022' + "6" + '\u0022' + " Type=" + '\u0022' + "PtzControl" + '\u0022' + "><PtzControl CameraId=" + '\u0022' + id.ToString() + '\u0022' + " CameraName=" + '\u0022' + "ALL" + '\u0022' + " PanCommand=" + '\u0022' + "Right" + '\u0022' + "/></CitiCommand>";
                    Preview.log.Info(texto);
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(texto);
                    //networkStream.Write(sendBytes, 0, sendBytes.Length);
                }
                else if (!networkStream.CanRead)
                {
                    
                    tcpClient.Close();
                }
                else if (!networkStream.CanWrite)
                {
                   
                    tcpClient.Close();
                }
            }
        }
        public static void mover_home(int id)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("10.158.64.53", 44000);
            }
            catch (SocketException)
            {
                //no se conecta con citilog
                return;
            }
            NetworkStream networkStream = tcpClient.GetStream();
            string texto, conf, mover;
            while (true)
            {
                if (networkStream.CanWrite && networkStream.CanRead)
                {

                    texto = "<CitiCommand Id=" + '\u0022' + "1" + '\u0022' + " Type=" + '\u0022' + "PtzControl" + '\u0022' + "><PtzControl CameraId=" + '\u0022' + id.ToString() + '\u0022' + " CameraName=" + '\u0022' + "ALL" + '\u0022' + " GotoPreset=" + '\u0022' + "1" + '\u0022' + "/></CitiCommand>";
                    
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(texto);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                }
                else if (!networkStream.CanRead)
                {

                    tcpClient.Close();
                }
                else if (!networkStream.CanWrite)
                {

                    tcpClient.Close();
                }

            }
        }
    }
}
