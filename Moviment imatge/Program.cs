using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Threading.Tasks.Dataflow;
using Heirloom;
using Heirloom.Desktop;

namespace LaPrincesa;

class Program
{
    const int HuevosOnScreen = 4;
    private static Window ventana;
    private static Yoshi yoshi = null;
    private static List<Huevo> _huevos= new();
    static Random posicionRandom = new Random();
    static void Main()
    {
        Application.Run(() =>
        {
            ventana = new Window("La ventana"); //, (1920, 1080));
            ventana.BeginFullscreen();
            
            yoshi = new Yoshi(10,660);
            //Yoshi.Scale = new Vector(50f baseWidth, 50f baseHeight);
            
            bool princesa = true;
            for (int i=0 ; i < HuevosOnScreen; i++)
            {
                
                _huevos.Add(new Huevo(princesa));
                princesa = false;
            }
            
            
            var loop = GameLoop.Create(ventana.Graphics, OnUpdate);
            loop.Start();
        });
        
    }
    
    private static int vidas = 4;

    private static void MostrarVentanaPrincesa()
    {
        var ventanaPrincesa = new Window("¡Princesa Yoshi!");
        ventanaPrincesa.BeginFullscreen();
        var imagenPrincesa = new Image("imagen/princesaYoshi.jpg ");
    
        var loopPrincesa = GameLoop.Create(ventanaPrincesa.Graphics, (gfx, dt) =>
        {
            var rectanguloVentana = new Rectangle(0, 0, ventanaPrincesa.Width, ventanaPrincesa.Height);
            gfx.DrawImage(imagenPrincesa, rectanguloVentana);
        });
        loopPrincesa.Start();
    }
    private static bool ventanaPrincesaMostrada = false;

    private static void YouLoose()
    {
        var hasPerdido = new Window("You Loose", (800, 600));
        var imagenPerdido = new Image("imagen/Loose.gif");
        hasPerdido.BeginFullscreen();
        var loopPerder = GameLoop.Create(hasPerdido.Graphics, (gfx, dt) =>
        {
            var rectanguloVentana = new Rectangle(0, 0, hasPerdido.Width, hasPerdido.Height);
            gfx.DrawImage(imagenPerdido, rectanguloVentana);
        });
        loopPerder.Start();
    }
    private static bool ventanaPerdidoMostrada = false;
    
    
    
    public static void ReiniciarHuevos()
    {
        foreach (var huevo in _huevos)
        {
            huevo.CambiarPosicion();
        }

        if (vidas > 0)
        {
            vidas--;
        }
        else
        {
            ventanaPerdidoMostrada = true; // Evitar infinitas ventanas
            YouLoose();
            ventana.Close();
        }
    }
    
    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
        var rectanguloVentana = new Rectangle
            (
                0, 0, ventana.Width, ventana.Height
            );
        
        var fondo = new Image("imagen/fondoMario.jpg");
        
        yoshi.Mover(rectanguloVentana);
        foreach (var huevo in _huevos)
        {
            if (yoshi.HuevoTocado(huevo))
            {
                if (huevo.EsPrincesa && !ventanaPrincesaMostrada)
                {
                    ventanaPrincesaMostrada = true; // Evitar infinitas ventanas
                    
                    MostrarVentanaPrincesa();
                    MostrarVentanaPrincesa();
                    ventana.Close();
                }
                else if (!ventanaPrincesaMostrada)
                {
                    ReiniciarHuevos();
                }
            }
        }
        //var fuente = new Font.Load("imagen/stocky.ttf", 30);
        //var numVidas = $"Vidas: {vidas}";
        //var posicionVidas = new Vector(ventana.Width - 200, ventana.Height - 50); // Ajusta según el tamaño de la ventana
        //gfx.DrawText(fuente, numVidas, posicionVidas, Color.White);
        
        
        
        gfx.DrawImage(fondo, rectanguloVentana);
        
        foreach (var huevo in _huevos)
        {
            huevo.Pintar(gfx);
            
        }
        yoshi.Pintar(gfx);
    }
}