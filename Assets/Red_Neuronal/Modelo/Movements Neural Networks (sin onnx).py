#!/usr/bin/env python
# coding: utf-8

# In[1]:


#Load Basic libraries
import numpy as np

#Data
import os
import csv

#Model
import keras
from keras.utils import to_categorical
from keras.utils import get_file
from keras.optimizers import SGD
from keras.models import Sequential
from keras.layers import Conv2D, MaxPooling2D, AveragePooling2D
from keras.layers import Dense, Flatten, Activation, Dropout

from keras.models import model_from_json

print("All libraries loaded!")


# In[2]:


#Función auxiliar que redimensiona un array dado
def flatten(dimData, moves):
    moves = np.array(moves)
    moves = moves.reshape(len(moves), dimData)
    moves = moves.astype('float32')
    moves /= 255
    return moves


# In[3]:


#Carga los datos
os.chdir("D:/Documentos/UCM/TFG/Red Neuronal")
os.getcwd()


moves, labels = [],[]
dirMoves = ["UP.csv", "DOWN.csv", "RIGHT.csv", "LEFT.csv"]

#Guarda todos los archivos de datos
for dir in dirMoves:
    #Este encoding es para que no genere caracteres extraños
    file = open(dirMoves[dirMoves.index(dir)], encoding='utf-8-sig')
    reader = csv.reader(file)

    for row in reader:
        #print(row)
        moves.append(row)
        labels.append(dirMoves.index(dir))
        


# In[4]:


#Separa el conjunto de datos en datos de prueba y de entrenamiento
#Se necesitaran mas de entrenamiento que de prueba
segregation, index = 5,0
train_moves, test_moves, train_labels, test_labels = [],[],[],[]

for move, label in zip(moves, labels):
    if index < segregation:
        train_moves.append(move)
        train_labels.append(label)
        index += 1
    else:
        test_moves.append(move)
        test_labels.append(label)
        index = 0

print('Numero de movimientos de entrenamiento: ', len(train_moves))
print('Numero de movimientos de prueba: ', len(test_moves))


# In[5]:


#"Aplana" los datos con la funcion auxiliar
dataDim = np.prod(len(moves[0]))
train_data  = flatten(dataDim, train_moves)
test_data = flatten(dataDim, test_moves)


train_labels = np.array(train_labels)
test_labels = np.array(test_labels)
train_labels_one_hot = to_categorical(train_labels)
test_labels_one_hot = to_categorical(test_labels)

#Determina el número de clases que se van a utilizar 
classes = np.unique(train_labels)
nClasses  = len(classes)


# In[6]:


#Para esta red neuronal se establecen tres capas, y 256 neuronas
model = Sequential()
model.add(Dense(256, activation = 'tanh', input_shape = (dataDim,)))
model.add(Dropout(0.2))
model.add(Dense(256, activation='tanh'))
model.add(Dropout(0.2))
model.add(Dense(256, activation='relu'))
model.add(Dropout(0.2))
model.add(Dense(nClasses, activation='softmax'))


epochs = 100
model.compile(optimizer='rmsprop', loss='categorical_crossentropy', metrics=['accuracy'])
history = model.fit(train_data, train_labels_one_hot, batch_size = 256, epochs=epochs, verbose=1,
                    validation_data=(test_data, test_labels_one_hot))

#test model
[test_loss, test_acc] = model.evaluate(test_data, test_labels_one_hot)
print("Resultado de la evaluación : Perdidas = {}, Precisión = {}".format(test_loss, test_acc))

model.summary()


# In[14]:


#Serializa el modelo para JSON
model_json = model.to_json()
model_path = "D:/Documentos/UCM/TFG/Red Neuronal/Modelo/model.json"
with open(model_path, "w") as json_file:
  json_file.write(model_json)

weights_path = "D:/Documentos/UCM/TFG/Red Neuronal/Modelo/model.h5"
#Serializa los pesos (weights) para HDF5
model.save_weights(weights_path)

#Carga el json y crea el modelo
json_file = open(model_path, 'r')
loaded_model_json = json_file.read()
 
json_file.close()
loaded_model = model_from_json(loaded_model_json)

#Carga los pesos (weights) en el nuevo modelo
loaded_model.load_weights(weights_path)

#Test
loaded_model.summary()


# In[ ]:




