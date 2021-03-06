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

        //Carga el modelo ya entrenado
        //string model_path = Application.dataPath + @"\Red_Neuronal\Modelo\model.json";
        //string weights_path = Application.dataPath + @"\Red_Neuronal\Modelo\model.h5";

        //var model = Sequential.ModelFromJson(File.ReadAllText(model_path));
        //model.LoadWeight(weights_path);

        //Predice el tipo de un movimiento dado y devuelve el valor
        //var ynew = model.Predict(Xnew);

        //move = (int)ynew[0];

        Tensor input = new Tensor(1, 1, 150, 1, Xnew);
        worker.Execute(input);

        Tensor output = worker.PeekOutput();
        Debug.Log(output.DataToString());
        
        //TODO: hacer dispose de todo lo usado (worker, output...) cuando ya no se use
        input.Dispose();

        return move;
    }

    public void Dispose()
    {
        worker.Dispose();
    }

        
}


