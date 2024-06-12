using OpenTK.Graphics.OpenGL4;
using System.Reflection.Metadata;

namespace OpenTKLearning
{
    public class Shader : IDisposable
    {
        private readonly int _handle;
        private bool disposedValue;

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexShaderSource = File.ReadAllText(vertexPath);
            string fragmentShaderSource = File.ReadAllText(fragmentPath);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            CompileShader(vertexShader, vertexShaderSource);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            CompileShader(fragmentShader, fragmentShaderSource);

            _handle = GL.CreateProgram();
            GL.AttachShader(_handle, vertexShader);
            GL.AttachShader(_handle, fragmentShader);
            GL.LinkProgram(_handle);

            GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out int code);
            if (code == 0)
            {
                string infoLog = GL.GetProgramInfoLog(_handle);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(_handle, vertexShader);
            GL.DetachShader(_handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        ~Shader()
        {
            if (!disposedValue)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }

        public void Use()
        {
            GL.UseProgram(_handle);
        }

        private static void CompileShader(int shader, string source)
        {
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int code);
            if (code == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                Console.WriteLine(infoLog);
            }
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(_handle, attribName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GL.DeleteProgram(_handle);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
