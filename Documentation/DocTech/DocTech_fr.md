# Table des Matières
- [**Aperçu du Projet**](#aperçu-du-projet)
- [**Technologies**](#technologies)
- [**Diagramme d'Architecture**](#diagramme-darchitecture)
- [**Diagramme de la Base de Données**](#diagramme-de-la-base-de-données)
- [**Prérequis**](#prérequis)
- [**Installation**](#installation)
- [**Docker**](#docker)
- [**Documentation de l'API**](#documentation-de-lapi)

## Aperçu du Projet
- **Nom** → YPlanning
- **Objectif** → Gérer les cours, permettre aux enseignants de noter et de faire l'appel de leurs élèves, tout en permettant aux étudiants de consulter leurs cours de la semaine ainsi que leurs notes.
- **Composants** → API en C#, conteneurs Docker, serveur PostgreSQL, serveur web Nginx, Infrastructure (DHCP, DNS, VLANs)

## Technologies
- **Framework Backend** → ASP.NET Core <br>
- **Base de Données** → Serveur PostgreSQL <br>
- **ORM** → Entity Framework Core <br>
- **Serveur Web + Reverse Proxy** → Serveur Nginx <br>
- **Plateforme de Déploiement** → Docker <br>
- **IDE Backend** → Visual Studio 2022
- **IDE Base de Données** → pgAdmin 4

## Diagramme d'Architecture
<img src="../../Infra/infra2.png">

- **Serveur DNS** → Utilisé pour traduire un nom de domaine en une adresse IP
- **Serveur de Messagerie** → Utilisé pour recevoir et gérer les emails
- **Serveur Web** → Utilisé pour héberger notre site web (interface)
- **Serveur de Base de Données** → Utilisé pour stocker ou accéder aux données

## Diagramme de la Base de Données
Voici le MCD (Modèle Conceptuel de Données) de la base de données
<img src="./Img/database.jpg">

Et voici le code SQL correspondant

```
CREATE TABLE USERS(
   id SERIAL,
   lastName VARCHAR(100) NOT NULL,
   firstName VARCHAR(100) NOT NULL,
   birthDate DATE NOT NULL,
   email VARCHAR(100) NOT NULL,
   phoneNumber VARCHAR(15),
   role VARCHAR(50) NOT NULL,
   PRIMARY KEY (id)
);

CREATE TABLE ACCOUNTS(
   id SERIAL,
   login VARCHAR(300) NOT NULL UNIQUE,
   password VARCHAR(300) NOT NULL,
   accountCreationDate DATE,
   lastLoginDate DATE,
   userId INTEGER NOT NULL,
   PRIMARY KEY(id),
   FOREIGN KEY(userId) REFERENCES USERS(id)
);

CREATE TABLE CLASSES(
   id SERIAL,
   subject VARCHAR(100) NOT NULL,
   classDate DATE NOT NULL,
   startTime TIME NOT NULL,
   endTime TIME NOT NULL,
   room VARCHAR(10) NOT NULL,
   PRIMARY KEY(id)
);

CREATE TABLE ATTENDANCES(
   id SERIAL,
   classId INTEGER NOT NULL,
   userId INTEGER NOT NULL,
   status VARCHAR(50) NOT NULL,
   reason VARCHAR(200),
   PRIMARY KEY(id),
   FOREIGN KEY(classId) REFERENCES CLASSES(id),
   FOREIGN KEY(userId) REFERENCES USERS(id)
);

CREATE TABLE TESTS(
   id SERIAL,
   classId INTEGER NOT NULL,
   userId INTEGER NOT NULL,
   score VARCHAR(50) NOT NULL,
   PRIMARY KEY(id),
   FOREIGN KEY(classId) REFERENCES CLASSES(id),
   FOREIGN KEY(userId) REFERENCES USERS(id)
);
```


## Prérequis

| Outil / Ressource                                                | Description |
|:----------------------------------------------------------------|:------------|
| [Docker](https://www.docker.com/products/docker-desktop/)       | Utilisé pour déployer l'API et le serveur PostgreSQL <br> *(version utilisée 4.30.0)* |
| [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/) | Obligatoire pour le fonctionnement de l'API **(choisir .NET 6.0)** |
| [pgAdmin 4](https://www.pgadmin.org/download/pgadmin-4-windows/) | Utilisé pour gérer et visualiser votre base de données PostgreSQL <br> *(choisir la dernière version)* |
| [Postman](https://www.postman.com/downloads/) | Utilisé pour accéder aux endpoints <br> *(choisir la dernière version)* |
| [Référentiel](https://github.com/MihutaMihail/YPlanning/)        | Cloner le référentiel sur votre ordinateur |

## Installation

**Avant de commencer, assurez-vous d'avoir accès à tous les outils / ressources spécifiés dans** [**Prérequis**](#prérequis)

Une autre chose à noter, dans le fichier /Dev/YPlanning/AppSetup/seed.sql, il y a certaines données qui ne sont pas insérées par défaut. Si vous souhaitez tester tous les points de terminaison, vous pouvez décommenter ces insertions afin d'avoir des données à manipuler.

1. Allez à **/Dev/YPlanning/AppSetup/seed.sql**. Voici à quoi ressemble le [fichier](/Dev/YPlanning/AppSetup/seed.sql). <br>
Suivez les instructions là-bas et revenez ici lorsque c'est indiqué. <br>
**À NOTER: FAITES CELA DANS LE RÉPERTOIRE CLONÉ**
2. Dans la même fenêtre PowerShell, choisissez l'option 1 → **INSTALLATION COMPLÈTE (RECOMMANDÉE)**
3. Il vous sera demandé de créer un certificat si vous n'en avez pas déjà un. Si vous en avez un, vous pouvez utiliser votre certificat existant.
4. Tout ce qui nécessitera une entrée concerne les mots de passe pour le certificat et la base de données. Vous devrez saisir le même mot de passe deux fois, une fois lors de la création du certificat et de la base de données, et une fois lors de la construction de l'API pour qu'elle puisse accéder aux éléments susmentionnés en utilisant le mot de passe que vous avez choisi à l'étape précédente.
5. **VOUS AVEZ TERMINÉ**. Les conteneurs API et PostgreSQL devraient être en cours d'exécution, vous pouvez visualiser la base de données en utilisant **pgAdmin 4** (utilisez le même mot de passe que celui choisi dans le script) ou même accéder à la documentation SWAGGER sur https://localhost:443/swagger/index.html
<br>
<br>

**SI CELA NE FONCTIONNE PAS**
- Un certificat valide et de confiance est *obligatoire*. En utilisant PowerShell, vous pouvez taper la commande suivante pour vous assurer que votre certificat est de confiance.

```
dotnet dev-certs https --trust --check
```
Si l'exécution de cette commande ne retourne pas un message du type "A trusted certificate was found: ...". Vous pouvez exécuter la commande suivante pour vous assurer que votre certificat est de confiance
```
dotnet dev-certs https --trust
```

- Vous devrez peut-être redémarrer votre navigateur. Parfois, Chrome (ou un autre navigateur) continuera à utiliser votre ancien certificat. Si vous avez généré un nouveau certificat, cela pourrait être la cause du problème.

- Assurez-vous que le mot de passe que vous avez choisi est le même à chaque fois qu'une saisie de mot de passe est demandée. Vous pouvez toujours réexécuter le script **setup.ps1** avec l'option suivante **1. INSTALLATION COMPLÈTE (RECOMMANDÉE)** pour tout réinstaller.

## Utilisation

**Avant de commencer, assurez-vous d'avoir suivi attentivement chaque étape de** [**Installation**](#installation)

Avant de commencer à appeler les endpoints souhaités, assurez-vous d'importer la [collection Postman](Documentation/YPlanning.postman_collection.json) fournie dans votre Postman. Vous pouvez télécharger le fichier json d'ici ou simplement choisir le fichier à partir du répertoire cloné lors de l'importation de la collection.

**Vous vous souvenez du jeton chiffré que vous avez inséré dans ce [fichier](/Dev/YPlanning/AppSetup/seed.sql) ?** <br>
Le but de cela était de créer un jeton *TEMPORAIRE* avec un accès de niveau administrateur afin que vous puissiez créer votre propre utilisateur et compte.

1. En utilisant la version non chiffrée du jeton copiée depuis **l'étape 1**, vous accéderez aux endpoints *CreateUser* et *CreateAccount* en utilisant **Postman**. Pour utiliser le jeton, allez dans la zone **Headers** de la requête, et insérez votre jeton dans la colonne **Value** (le nom a déjà été inséré pour vous)
2. Maintenant, vous pouvez créer votre utilisateur et compte avec vos propres données <br>
*(À NOTER: assurez-vous que l'userId correspond à l'Id du nouvel utilisateur créé)*
3. Une fois que les deux ont été créés, vous pouvez maintenant accéder au endpoint **Login** où vous devrez insérer votre identifiant et mot de passe de compte. Si la connexion réussit, un jeton sera retourné. Ce jeton vous sera donné tant que vous ne supprimez pas votre propre compte, donc vous n'avez pas besoin de le stocker, mais vous pouvez le faire si vous le souhaitez.
4. **VOUS AVEZ TERMINÉ**. Vous avez maintenant créé votre propre utilisateur et son compte. Vous avez également réussi à vous connecter à votre compte et votre jeton a été généré. En utilisant ce jeton, vous pouvez supprimer l'utilisateur *TEMPORAIRE* (le jeton sera supprimé avec).

**IMPORTANT**
- Lors de la création de l'utilisateur, assurez-vous de lui attribuer un rôle avec une valeur égale à **admin**, **student** ou **teacher** (sensible à la casse). Sinon, votre jeton ne sera pas reconnu et sera inutile.

- N'oubliez pas que certains endpoints ne peuvent être utilisés qu'avec un jeton de niveau administrateur (par ex. CreateUser). Si vous créez un utilisateur avec un rôle de **student**, vous pourrez utiliser certains endpoints, mais pas tous.

## Docker
- Les principaux composants de Docker que nous allons utiliser sont les **Images** et les **Containers**.

### Images
Une image Docker est un package exécutable autonome qui comprend tout ce qui est nécessaire pour exécuter un logiciel. Cela inclut le code lui-même, le runtime, les bibliothèques, les variables d'environnement et les fichiers de configuration. D'une certaine manière, c'est similaire à la façon dont un fichier .iso contient tous les composants nécessaires pour configurer un système d'exploitation ou comment une classe en programmation orientée objet définit un modèle ou un modèle pour créer cette classe. Ces images seront utilisées pour créer des conteneurs Docker.

### Containers
En poursuivant avec l'analogie de la classe, vous pouvez considérer les conteneurs comme les instances de cette classe. Chaque conteneur est une instance en cours d'exécution de l'image avec son propre état et ses données. Cela signifie que plusieurs conteneurs peuvent être créés à partir de la même image tout comme plusieurs objets peuvent être instanciés à partir de la même classe.

Dans notre projet, nous avons deux conteneurs, **YPlanning** (API) et **postgres** (serveur PostgreSQL).

- Mais comment ces images sont-elles créées ? Dans le contexte de notre API par exemple, elle est créée en utilisant la commande suivante

```
docker build -t IMAGE_NAME DOCKERFILE_DIR_LOCATION
```

Dans le contexte de l'API, le DOCKERFILE sera différent des autres. Le DOCKERFILE en C# compilera d'abord l'application .NET pour ensuite la déployer et enfin créer l'image runtime finale. Après cette étape, vous pouvez exposer certains ports comme le 443 pour HTTPS, vous pourriez ajouter certaines variables d'environnement qui ne peuvent être accessibles qu'à l'intérieur du conteneur créé ou simplement copier n'importe quel fichier. Pour finir, vous devez définir un point d'entrée qui serait un fichier .dll qui va démarrer tout ce que le DOCKERFILE a compilé.

## Configuration du Serveur
- Montrer et expliquer chaque configuration du serveur

## Documentation de l'API
Pour le moment, la documentation de l'API n'est pas disponible. Cependant, une documentation SWAGGER générée automatiquement contenant tous les endpoints peut être consultée [ici](https://mihutamihail.github.io/YPlanning).

À NOTER: cette documentation SWAGGER n'aura pas la même apparence que la documentation SWAGGER que vous utiliserez sur https://localhost:443/swagger/index.html. En effet, il s'agit d'une représentation HTML du code JSON que vous pouvez trouver [ici](../swagger.json) en utilisant [Swagger Editor](https://editor.swagger.io/).
