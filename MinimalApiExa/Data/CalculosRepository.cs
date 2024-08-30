using MinimalApiExa.Repository;

namespace MinimalApiExa.Data
{
    public class CalculosRepository : IcalculosRepository
    {
        /// <summary>
        /// Este método toma dos parámetros de tipo int y devuelve un valor entero que es el resultado de la multiplicación
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int multiplicar(int a, int b)
        {
            int c = 0;
            c = a * b;
            return c;
        }
    }
}
