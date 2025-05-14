
# Application de Gestion de Boutique Florale

Cette application desktop a été développée en **C#** avec **WPF**, en suivant l’architecture **MVVM**, et en utilisant **Entity Framework** pour la gestion des données. Elle permet de gérer les fleurs, les bouquets, les vendeurs et les commandes clients. Des données peuvent également être importées via des **API REST**.

---

## 🚀 Fonctionnalités

- Interface utilisateur moderne construite avec WPF.
- Architecture MVVM assurant une séparation claire des responsabilités.
- Gestion des entités : fleurs, bouquets, vendeurs, commandes.
- Possibilité de créer des commandes avec plusieurs articles (fleurs et bouquets) et un vendeur unique.
- Intégration d'API REST pour l'importation dynamique des données.
- Utilisation d’Entity Framework pour la persistance des données dans une base SQL Server.

---

## 🖥️ Prérequis

Avant de lancer le projet, vous devez avoir :

- [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/fr/vs/community/)
  - Workload **Développement .NET de bureau**
- [.NET Desktop Runtime 6.0 ou version supérieure](https://dotnet.microsoft.com/en-us/download)
- SQL Server Express ou une instance SQL Server locale
- Connexion internet (pour les appels API)

---

## ⚙️ Configuration

1. **Cloner le projet** :
   ```bash
   git clone https://github.com/votre-utilisateur/nom-du-repo.git
   ```

2. **Ouvrir le projet dans Visual Studio Community** :
   - Ouvrez le fichier `.sln` avec Visual Studio.

3. **Configurer la chaîne de connexion** :
   - Dans `App.config` (ou `appsettings.json` si utilisé), modifiez la chaîne de connexion à votre base SQL Server.

4. **Appliquer les migrations Entity Framework** (si applicable) :
   - Ouvrez la **Console du gestionnaire de package NuGet** :
     ```powershell
     Update-Database
     ```

---

## ▶️ Lancement de l’application

- Cliquez sur **Démarrer (Start)** ou appuyez sur **F5** dans Visual Studio Community.

---

## 🛠️ Technologies utilisées

- C# avec WPF
- MVVM (Model-View-ViewModel)
- Entity Framework Core
- API REST (via `HttpClient`)
- SQL Server / SQL Express

---