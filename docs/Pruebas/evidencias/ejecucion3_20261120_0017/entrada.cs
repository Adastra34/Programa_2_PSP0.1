using System;

public interface IRepositorio
{
    void Guardar();
}

public struct Punto2D
{
    public int X;
    public int Y;
}

public record Usuario(string Nombre, int Edad);
