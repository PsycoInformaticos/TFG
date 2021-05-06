//using Keras.Models;
using System.IO;
using UnityEngine;
using Unity.Barracuda;


public class NeuralNetwork : MonoBehaviour
{
    public NNModel model;
    private Model m_RuntimeModel;
    private IWorker worker;

    //True: muestra por consola todo lo que va haciendo el modelo. False: no saca por consola la informacion
    bool verbose = false;

    void Start()
    {
        m_RuntimeModel = ModelLoader.Load(model, verbose);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, m_RuntimeModel, verbose);
    }

    public int Movement(float [] Xnew)
    {
        int move = -1;

        // Batch = 1 por entrar un solo movimiento
        // Height = 1 ya que es un vector unidimensional
        // Width = 150 es la longitud de cada vector de movimiento
        // Channels hace referencia a los valores de los colores, por eso es 0 al no ser una imagen
        // Luego pasamos el propio vector de movimiento (X nueva para averiguar la Y)
        Tensor input = new Tensor(1, 1, 150, 0, Xnew);
        worker.Execute(input);

        Tensor output = worker.PeekOutput();
        string data = output.DataToString();
        Debug.Log(data);

        float f1 = 0;
        for (int i = 0; i < output.length; i++)
        {
            float f2 = output[i];

            if (f1 < f2)
            {
                f1 = f2;
                
                if (f1 >= 1)
                {
                    move = i;
                }
            }
        }

        input.Dispose();
        output.Dispose();

        //Debug.Log(move);
        return move;
    }

    public void Dispose()
    {
        worker.Dispose();
    }

        
}


