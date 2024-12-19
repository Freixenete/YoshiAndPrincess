using System.Numerics;
using Heirloom;
using Vector = Heirloom.Vector;

namespace LaPrincesa;

public class Princesa
{
    private readonly Image imagen;
    private Vector posicion;

    public Princesa(int x, int y)
    {
        imagen = new Image("imagen/yoshiRosa.png");
    }
    
    public static void Pintar(GraphicsContext gfx)
    {
        Princesa.Pintar(gfx);
        //gfx.DrawImage();
    }
}