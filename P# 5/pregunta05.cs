using System; // Importa el espacio de nombres System
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic
using System.Linq; // Importa el espacio de nombres System.Linq
using System.Text; // Importa el espacio de nombres System.Text
using System.Threading.Tasks; // Importa el espacio de nombres System.Threading.Tasks
using System.Threading; // Importa el espacio de nombres System.Threading

//INF-317 - 1ER EXAMEN PARCIAL
//NOMBRE: VICTOR GABRIEL CAPIA ALI
//CI: 4762494 LP
//PREGUNTA 5. La numeral 4 (PREGUNTA 4) con NET manejando hilos

namespace ConsoleApplication1 // Define un espacio de nombres llamado ConsoleApplication1
{
    class Program // Define una clase llamada Program
    {
        static void Main() // El método principal del programa
        {
            // Definimos la frase original
            string frase = "tres tristes tigres trigaban trigo por culpa del bolivar";

            // Definimos las cadenas para las dos frases resultantes
            StringBuilder frase1 = new StringBuilder();
            StringBuilder frase2 = new StringBuilder();

            // Definimos un arreglo para almacenar las palabras
            string[] palabras = new string[20];
            int num_palabras = 0;

            // Tokenizar la frase original
            string[] tokens = frase.Split(' '); // Divide la frase en palabras utilizando un espacio como delimitador
            foreach (string token in tokens)
            {
                palabras[num_palabras] = token; // Almacena la palabra en el arreglo
                num_palabras++; // Incrementa el contador de palabras
            }

            // Crear dos hilos para dividir las palabras
            Thread thread1 = new Thread(() =>
            {
                for (int i = 0; i < num_palabras; i += 2)
                {
                    lock (frase1) // Bloquea la variable frase1 para evitar conflictos de escritura entre hilos
                    {
                        frase1.Append(palabras[i]); // Agrega la palabra a la primera frase
                        frase1.Append(" "); // Agrega un espacio después de la palabra
                    }
                }
            });

            Thread thread2 = new Thread(() =>
            {
                for (int i = 1; i < num_palabras; i += 2)
                {
                    lock (frase2) // Bloquea la variable frase2 para evitar conflictos de escritura entre hilos
                    {
                        frase2.Append(palabras[i]); // Agrega la palabra a la segunda frase
                        frase2.Append(" "); // Agrega un espacio después de la palabra
                    }
                }
            });

            // Crear un ManualResetEvent para esperar a que los hilos terminen
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            // Manejar el evento cuando ambos hilos terminen
            thread1.Start(); // Inicia el primer hilo
            thread2.Start(); // Inicia el segundo hilo
            ThreadPool.QueueUserWorkItem((state) =>
            {
                thread1.Join(); // Espera a que termine el primer hilo
                thread2.Join(); // Espera a que termine el segundo hilo
                manualResetEvent.Set(); // Establece el evento de reinicio manual para indicar que ambos hilos han terminado
            });

            // Esperar a que ambos hilos terminen
            manualResetEvent.WaitOne(); // Espera a que el evento de reinicio manual se active (ambos hilos han terminado)

            // Imprime las dos frases resultantes
            Console.WriteLine("Frase 1: " + frase1.ToString()); // Imprime la primera frase
            Console.WriteLine("Frase 2: " + frase2.ToString()); // Imprime la segunda frase

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey(); // Espera a que el usuario presione una tecla antes de salir
        }
    }
}
