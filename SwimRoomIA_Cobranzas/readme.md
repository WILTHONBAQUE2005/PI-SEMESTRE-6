# PI Semestre 6 – CobranzasPro

Sistema web para gestión de **ventas diferidas y riesgo de mora** apoyado con un modelo de **Inteligencia Artificial**.  
El proyecto incluye:

- API de IA en **Python + FastAPI** para predecir riesgo de mora.
- Aplicación web **ASP.NET Core MVC** (`SwimRoomWeb`) para el portal de analistas de cobranzas.
- Base de datos **MariaDB/MySQL** (XAMPP) para almacenar:
  - Historial de cobranzas usado por el modelo.
  - Ventas diferidas evaluadas por la IA.

---

## Tecnologías principales

- Python 3.x (se usó Python 3.13 en el desarrollo).
- FastAPI, Uvicorn, scikit-learn, pandas, numpy, mysql-connector-python.
- .NET SDK (target framework `net10.0`) con ASP.NET Core MVC.
- Entity Framework Core + Pomelo.EntityFrameworkCore.MySql.
- MariaDB / MySQL (por ejemplo via XAMPP).
- Visual Studio Code.

---

## Estructura del proyecto

```text
Semana 8/
├─ db/
│  ├─ swimroom_ia.sql       # BD histórica para el modelo
│  └─ swimroom_app.sql      # BD de la aplicación MVC
│
├─ SwimRoomIA_Cobranzas/
│  ├─ entrenar_modelo.py
│  ├─ cargar_datos_prueba.py
│  ├─ api/
│  │  └─ main.py            # API FastAPI para predicción de riesgo
│  └─ requirements.txt      # Dependencias Python
│
└─ SwimRoomWeb/             # Aplicación ASP.NET Core MVC
   ├─ Controllers/
   ├─ Models/
   ├─ Views/
   ├─ wwwroot/
   └─ appsettings.json

1. Clonar el repositorio:

git clone https://github.com/WILTHONBAQUE2005/PI-SEMESTRE-6.git
cd "PI-SEMESTRE-6"   # o la carpeta que corresponda


2. Restaurar BD desde db/swimroom_ia.sql y db/swimroom_app.sql.

3. Instalar dependencias Python:

cd SwimRoomIA_Cobranzas
python -m venv venv
venv\Scripts\activate
pip install -r requirements.txt


4. Levantar API de IA:

cd api
uvicorn main:app --reload --port 8000


5. En otra terminal, ejecutar la app MVC:

cd SwimRoomWeb
dotnet run


6. Abrir el navegador en la URL indicada (por ejemplo http://localhost:5111).