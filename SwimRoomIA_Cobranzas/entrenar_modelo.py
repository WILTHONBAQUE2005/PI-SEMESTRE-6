import mysql.connector
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import accuracy_score, classification_report
import joblib
import os
import sys

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

if df.empty:
    print("⚠ No hay datos en la tabla historico_cobranza. Primero carga datos de prueba.")
    sys.exit(1)

X = df.drop(columns=["tiene_mora"])
y = df["tiene_mora"]

X_train, X_test, y_train, y_test = train_test_split(
    X, y, test_size=0.2, random_state=42
)

model = LogisticRegression(max_iter=1000)
model.fit(X_train, y_train)

y_pred = model.predict(X_test)
print("Accuracy:", accuracy_score(y_test, y_pred))
print(classification_report(y_test, y_pred))

os.makedirs("modelos", exist_ok=True)
joblib.dump(model, "modelos/modelo_riesgo_mora.joblib")
print("✅ Modelo guardado en modelos/modelo_riesgo_mora.joblib")
