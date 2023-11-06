using System.Collections;  
using System;              
using System.Threading;    

//INF-317 - 1ER EXAMEN PARCIAL
//NOMBRE: VICTOR GABRIEL CAPIA ALI
//CI: 4762494 LP
//PREGUNTA 5. La numeral 4 (PREGUNTA 4) con NET manejando hilos

class Program
{
    static void Serie(object state)  // Define un método llamado 'Serie' que acepta un objeto 'state' como argumento.
    {
        var valores = (Tuple<long, long>)state;  // Convierte 'state' en una tupla de dos valores long.
        long a = valores.Item1;  // Obtiene el primer valor de la tupla.
        long b = valores.Item2;  // Obtiene el segundo valor de la tupla.

        long resultado1 = (a * a) + 1;  
        long resultado2 = 2 * b;        

        Console.WriteLine(resultado1);  // Imprime resultado1 en la consola.
        Console.WriteLine(resultado2);  // Imprime resultado2 en la consola.
    }

    static void Main()  // Define el método principal 'Main'.
    {
        int inicio = 1;     // Define una variable 'inicio' con el valor 1.
        int fin = 10000;     // Define una variable 'fin' con el valor 5000.

        for (int i = inicio; i <= fin/2; i++)  // Un bucle que va desde 'inicio' hasta 'fin'.
        {
            long x = i;  // Asigna el valor de 'i' a la variable 'x'.
            long y = i;  // Asigna el valor de 'i' a la variable 'y'.
            ThreadPool.QueueUserWorkItem(Serie, new Tuple<long, long>(x, y));  // Agrega un trabajo en el ThreadPool para ejecutar el método 'Serie' con los argumentos 'x' y 'y'.
            // Pausa el hilo actual durante 1 milisegundo 
            //para mantener la secuecia de la serie 2,2,3,4,10,6,.
            Thread.Sleep(1);  
        }

        Console.ReadKey();  // Espera a que el usuario presione una tecla antes de finalizar la ejecución.
    }
}
