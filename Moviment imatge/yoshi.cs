using System.Numerics;
using Heirloom;
using Vector = Heirloom.Vector;

namespace LaPrincesa;

public class Yoshi
{
    public readonly Image imagen;
    private Vector posicion;
    private readonly int velocidad;
    public readonly Image imagenPrincesa;

    public Yoshi(int x, int y)
    {
        imagen = new Image("imagen/yoshi.png");
        velocidad = 4;
        posicion = new Vector(x, y);
        
        imagenPrincesa = new Image(path:"imagen/yoshiRosa.png");
    }


    public void Pintar(GraphicsContext gfx)
    {
        gfx.DrawImage( imagen, posicion);
    }
    
    public void Mover(Rectangle ventana)
    {
        //var mida = new Rectangle(40, 69);
        var nuevaPosicion = new Rectangle(posicion, imagen.Size);
        if (Input.CheckKey(Key.A, ButtonState.Down))
        {
            nuevaPosicion.X -= velocidad;
        }
        
        if (Input.CheckKey(Key.D, ButtonState.Down))
        {
            nuevaPosicion.X += velocidad;
        }
        
        if (Input.CheckKey(Key.W, ButtonState.Down))
        {
            nuevaPosicion.Y -= velocidad;
        }
        //else
        //{
        //    nuevaPosicion.Y += velocidad;
        //}
        
        if (Input.CheckKey(Key.S, ButtonState.Down))
        {
            nuevaPosicion.Y += velocidad;
        }

        if (ventana.Contains(nuevaPosicion))
        {
            posicion.X = nuevaPosicion.X;
            posicion.Y = nuevaPosicion.Y;
        }
    }

    public bool HuevoTocado(Huevo huevo)
    {
        var rectanguloYoshi = new Rectangle(posicion, imagen.Size);
        var rectanguloHuevoYoshi = huevo.Posicion();

        
        return rectanguloYoshi.Overlaps(rectanguloHuevoYoshi);
    }
}