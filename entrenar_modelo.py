import mysql.connector
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import accuracy_score, classification_report
import joblib
import os

# Conexión a MariaDB
conn = mysql.connector.connect(
    host="localhost",
    user="root",
    password="",
    database="swimroom_ai"
)

query = """
SELECT 
    monto_compra,
    num_cuotas,
    ingreso_mensual,
    dias_atraso_max,
    porcentaje_cuotas_pagadas,
    tiene_mora
FROM historico_cobranza;
"""

df = pd.read_sql(query, conn)
conn.close()

# Separar variables de entrada (X) y salida (y)
X = df.drop(columns=["tiene_mora"])
y = df["tiene_mora"]

# División entrenamiento/prueba
X_train, X_test, y_train, y_test = train_test_split(
    X, y, test_size=0.2, random_state=42
)

# Modelo de regresión logística
model = LogisticRegression(max_iter=1000)
model.fit(X_train, y_train)

# Evaluación rápida
y_pred = model.predict(X_test)
print("Accuracy:", accuracy_score(y_test, y_pred))
print(classification_report(y_test, y_pred))

# Guardar modelo entrenado
os.makedirs("modelos", exist_ok=True)
joblib.dump(model, "modelos/modelo_riesgo_mora.joblib")
print("Modelo guardado en modelos/modelo_riesgo_mora.joblib")
