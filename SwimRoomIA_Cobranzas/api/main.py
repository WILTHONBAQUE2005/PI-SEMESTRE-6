from fastapi import FastAPI
from pydantic import BaseModel
import joblib
import numpy as np
import os

app = FastAPI()

ruta_modelo = os.path.join(os.path.dirname(__file__), "..", "modelos", "modelo_riesgo_mora.joblib")
modelo = joblib.load(ruta_modelo)

class DatosCliente(BaseModel):
    monto_compra: float
    num_cuotas: int
    ingreso_mensual: float
    dias_atraso_max: int
    porcentaje_cuotas_pagadas: float

@app.post("/predict_riesgo")
def predecir_riesgo(datos: DatosCliente):
    X = np.array([[ 
        datos.monto_compra,
        datos.num_cuotas,
        datos.ingreso_mensual,
        datos.dias_atraso_max,
        datos.porcentaje_cuotas_pagadas
    ]])
    prob = modelo.predict_proba(X)[0][1] 

    if prob < 0.3:
        nivel = "bajo"
    elif prob < 0.7:
        nivel = "medio"
    else:
        nivel = "alto"

    return {
        "probabilidad_mora": float(prob),
        "nivel_riesgo": nivel
    }
