using MinimalApiExa.Repository;

namespace MinimalApiExa.Data
{
    public class CalculosRepository : IcalculosRepository
    {
        // Este método toma dos parámetros de tipo int y devuelve un valor entero que es el resultado de la multiplicación
        public int multiplicar(int a, int b)
        {
            int c = 0;
            c = a * b;
            return c;
        }
    }
}
