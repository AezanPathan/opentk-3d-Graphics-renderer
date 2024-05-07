using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace opentk_3d_Graphics_renderer
{
    internal class Window : GameWindow
    {

        float[] vertices =
        {
             0f,0.5f,0f,       /// top left vertex
            -0.5f,-0.5f,0f, /// bottom left vertex
             0.5f,-0.5f,0f /// bottom right vertex
        };
      
        /// renderer piplnie variables
        int vao, shaderProgram;

        /// width and height of the screen
        private int Width, Hight;

        public Window(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Title = " 3d graphic renderer by Aezan";
            this.CenterWindow(new Vector2i(width, height));
            this.Width = width;
            this.Hight = height;
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.Width = e.Width;
            this.Hight = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length *sizeof(float), vertices,BufferUsageHint.StaticDraw);

            ///bind the vao
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float,false,0,0);
            GL.EnableVertexArrayAttrib(vao, 0);
             
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); /// unbind vbo
            GL.BindVertexArray(0); ///unbind the vao
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.5f, 0.5f, 0.5f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }
        // Function to load a text file and return its contents as a string
        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }
    }
}
