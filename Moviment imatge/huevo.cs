using System.Numerics;
using Heirloom;
using Vector = Heirloom.Vector;

namespace LaPrincesa;

public class Huevo
{
    public Image imagen;
    public readonly Image imagenPrincesa;
    private Vector posicion;
    public bool EsPrincesa { get; }
    
    Random posicionRandom = new Random();
   
    public Huevo(bool esPrincesa)
    {
        imagen = new Image("imagen/huevoYoshi(Pequeña).png");
        posicion = new Vector(posicionRandom.Next(maxValue:1920 - imagen.Width), 
                              posicionRandom.Next(maxValue:(1080) - imagen.Height));
        
        imagenPrincesa = new Image(path:"imagen/yoshiRosa.png");
        //imagenPrincesa(imagenPrincesa.Width *2, imagenPrincesa.Height * 2);
        EsPrincesa = esPrincesa;
        
    }
    public void CambiarPosicion()
    {
        posicion = new Vector(posicionRandom.Next(maxValue: 1920 - imagen.Width), 
                              posicionRandom.Next(maxValue: 1080 - imagen.Height));
    }

    public Rectangle Posicion()
    {
        return new Rectangle(posicion, imagen.Size);
    }
    
    public void Pintar(GraphicsContext gfx)
    {
        gfx.DrawImage( imagen, posicion);
    }
    
    public bool HuevoOverlaps(Huevo huevo)
    {
        var rectanguloHuevoYoshi = new Rectangle(posicion, imagen.Size);
        return rectanguloHuevoYoshi.Overlaps(rectanguloHuevoYoshi);
    }
    
    public void CambiarImagen(string nuevaRuta)
    {
        imagen = new Image("imagen/yoshiRosa.png");
    }
    
}