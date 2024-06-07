# Table of Contents
- [**18/05/2024**](#18052024)
- [**23/05/2024**](#23052024)
- [**24/05/2024**](#24052024)
- [**25/05/2024**](#25052024)
- [**26-27/05/2024**](#26-27052024)
- [**28/05/2024**](#28052024)
- [**29/05/2024**](#29052024)
- [**31/05/2024**](#31052024)
- [**01/06/2024**](#01062024)
- [**02/06/2024**](#02062024)
- [**03/06/2024**](#03062024)
- [**04/06/2024**](#04062024)
- [**05/06/2024**](#05062024)
- [**06/06/2024**](#06062024)
- [**07/06/2024**](#07062024)

## 18/05/2024
J’ai décidé d’utiliser un ORM (Object Relational Mapper). Le ORM choisi sera Entity Framework (EF) Core. La raison est que ORM, contrairement à un ORM comme Dapper, est du LINQ (Integrated Language Query) ce qui signifie qu’on peut écrire des requêtes SQL en utilisation de la syntaxe C#. Dapper a bien sûr d’autres avantages mais je pense que même si un ORM comme EF n’est pas forcément nécessaire (vu la taille de notre projet), je pourrais essayer de l’utiliser et donc gagner de l’expérience dans le domaine de ORM.

De plus, un framework sera utilisé pour cette API. Le framework utilisé est ASP.NET Core. Comparé à l’autre API que j’ai fait sans framework, en utiliser en pour ce projet me donne la possibilité d’utiliser des nouveaux outils ainsi qu'avoir un meilleur temps développer une API plus performant à l’aide du framework.

En ce qui concerne le stack voici un : <br>
<ins>Backend Framework</ins> → ASP.NET Core <br>
<ins>Database</ins> → SQL Server <br>
<ins>ORM</ins> → Entity Framework (EF) Core <br>
<ins>Web Server</ins> → Apache Server <br>
<ins>Deployment Platform</ins> → Web Server (local) <br>
<ins>IDE</ins> → Visual studio

## 23/05/2024
Ce jour a été utilisé pour encore plus de recherches, donc pas d’avancement sur le code, mais je pense que c’est pour le mieux, il faut qu’on soit bien préparé avant de commencer un projet avec un bon nombre de composants qu’on doit mettre en place.

Premièrement, j’ai installé Docker et regardé comment il fonctionne. Il est basé sur un système des images et containers (conteneurs). L’image est la “machine” ou le service en question (ex: serveur base de données, serveur web, etc) et le container est basé sur l’image. Ceci nous permet d’avoir une seule image, peut plusieurs containers ce qui signifie plusieurs serveurs qui fonctionnent en même temps. Un container peut aussi posséder des volumes qui sont utilisés comme espace de stockage. On va probablement avoir besoin de 3 images.

→ 1 pour l’API <br>
→ 1 pour le front (l’application web) <br>
→ 1 pour le serveur Nginx (ou Apache) qui sera utilisé en tant que serveur web mais aussi reverse proxy.

Sur le sujet du proxy, j’ai fait des recherches pour mieux comprendre la différence entre le proxy inverse et proxy “normale”. Le proxy inverse agit sur le nom des serveurs et le proxy  “normale” agit sur le nom du client. Ce proxy peut être utilisé pour contourner certaines restrictions comme le CORS alors que le proxy inverse va agir comme le serveur unique qui va diriger les requêtes des utilisateurs vers le serveur correspondant (dans notre cas, API ou front). On va avoir besoin d’un reverse proxy.

D’après ce que j’ai trouvé, Nginx semble être fait pour être un reverse proxy, donc ceci sera probablement notre choix (consomme moins de RAM, plus performant). Par contre, Apache peut aussi servir comme serveur de reverse proxy.

## 24/05/2024
Avant de commencer, je me suis assurer qu'on a tous les fichiers nécessaires dans notre répo comme la documentation technique, la charte, fiche de suivi, etc. Chaque fichier de documentation aura une version en français et une en anglais. J'ai aussi pensé a ce qu'on pourrait ajouter dans ces documentations (le readme du répo inclus) parce qu'on devra mettre à jour ces fichiers dés qu'on peut, il faut pas les laisser à la fin du projet. C'est comme si on avait un template.

La charte du projet a aussi été mise à jour, j'ai ajouté les technologies utilisées ainsi qu'un planning prévisionnel.

## 25/05/2024
J'ai fait un diagramme de GANTT pour le chapitre "planning prévisionnel" pour la charte.

J'ai installé tout ce qu'il faut et un projet ASP.NET Core a été créer dans le répertoire. Pour le moment, je suis en train de faire fonctionner l'API template qui est donné dans Docker.

## 26-27/05/2024
J'ai réussi (27/05) à faire marcher l'API template de ASP.NET Core sur HTTPS dans Docker. Le script powershell prend en compte d'autres situations comme par exemple donnée la posibilité au utilisateur de ne pas recréer un certificate puisqu'il en a déjà un et d'autres choses.

Faire marcher l'API sur Docker a été accompli plutôt facilement. Le gros problème été pour le faire fonctionner sur HTTPS. Il y a eu beaucoup de choses pour prendre en compte dans le script powershell et donc aussi dans Docker pour pouvoir utiliser des secrets (pour mettre le mot de passe du certificat), pour prendre ce fichier là et l'utiliser dans Docker pour pouvoir lire le certificat et donc l'utiliser et vers la fin des problèmes de caches. Chrome ne voulais pas mettre à jour le certificat après avoir été créer et donc beaucoup du temps a été dépensé pour essayer des nouvelles choses alors que le script fonctionnait, je devais juste relancer Chrome.

## 28/05/2024
J'ai mis à jour quelques fichiers du dossier documentation.
On (moi et mon collègue) a fait le MCD pour la base de données.
De plus, je me suis intérésée comment je pourrais faire fonctionner un serveur Postgres sur Docker et aussi au IDE que je pourrait utiliser observer la base de données.

## 29/05/2024
Mise à jour du script pour intégrer la base de données PostgresSQL dans Docker. Le script powershell maintenant démarre les conteneurs lors de l'installation, plus besoin de mettre manuellement port 443 pour le l'API. Installation du pg admin 4 pour accèder à la base de données de PostgresSQL. Lors de l'installation (dans le script), je créer la base de données et les tables nécessaires. Je vais probablement ajouter du seeding plus tard.

## 31/05/2024
Optimisation du code pour le script d'installation et l'ajout d'un menu de cette façon je peux construire que l'image de l'API si j'ai fait des modifications sur l'API mais pour les autres composants comme la base de données ou le certificat. Fait les tables de la base de données dans c# mais le problème qu'apparament c'est pas si simple. Il faut aussi prendre en compte les relations / cardinalités entre les tables et donc les mettres dans l'API.

## 01/06/2024
Refait les relations et cardinalités pour la base de données. Fait les modèles des tables pour l'API. Utilisation du Entity Framework pour faire le data context qui fait référance à la base de données. Il y a aussi le OnModelCreating() qui est rempli pour que Entity Framework fasse les correctes relations entre les tables.

## 02/06/2024
J'ai mis à jour le code SQL pour la base de données vu qu'elle a été modifié. J'ai essayer de me connecter à la base de donées postgres depuis l'API j'ai réussi. J'ai eu un problème que je me suis pas rendu compte pour un bon moment mais le problème c'été le host. Je suppose comme le server postgres n'est pas sur notre machine techniquement vu que c'est dans Docker, localhost, donc l'adresse 127.0.0.1 ne va pas fonctionner. Pour cela, il changer le host avec l'adresse du server postgres qui se trouve dans le fichier de configuration.

J'ai fait en sorte que l'adresse IP du serveur PostgreSQL est automatiquement prise le moment où le conteneur de l'API est créer. De cette façon, n'importe l'adresse IP que le serveur PostgreSQL aura, ça devra fonctionner.

J'ai changé les noms des entités au pluriel même si on n'a pas besoin de changer, parfois il y a des mots comme *contraint* ou *user* qui ne peuvent pas être utiliser comme un nom de table vu qu'il est résérver. Les noms d'entités comme *contraint**s*** ou *user**s***, ne vont pas poser des problèmes.

J'ai créer le premier Controller pour les users dans l'API. J'ai utilisé des interfaces pour définir les méthodes utilisées qui vont être faite par les repositories mais aussi pour appeler ces méthodes. C'est comme si l'interface est le front qui va tout définir mais aussi appeller le back (juste le repository) pour utiliser sa fonction.
Pour finir, tout ça a été utilisé dans un controller pour pouvoir faire un simple get pour avoir tous les users.

Pour finir, j'ai dû indiqué à entity framework d'utiliser des miniscules pour les tables mais aussi pour les colonnes parce que postgres mais tous en miniscule par défaut mais entity framework met la première lettre en majuscule et donc il n'arrive pas a trouver les tables ou colonnes sans définir ces choses là.

## 03/06/2024
Endpoint créer pour avoir un utilisateur par son id. Ajout du AutoMapper pour plus facilement utiliser des Dto (data transfer object). Vu qu'on a du spécifié dans les modèles les relations entre les tables (ex: le modèle USER a une colonne Account parce qu'il est en relation avec la table Account: relation 1-1), on ne veut pas envoyer ces données qu'on on accède un endpoint comme api/user ou api/user/{userId}. Avec les Dto, on peut spécifier ce qu'on veut envoyer et le AutoMapper va faire ceci automatiquement. Comme ça, quand on accède un endpoint comme api/user, on nous envoie que les colonnes qu'on a choisi. Avec ceci, on peut aussi créer des multiples Dto pour la même entité. On peut avoir un BasicUserDto qui va nous montrer juste les détailles basiques d'un user, mais si on est connecté avec notre compte, on va nous montrer le dto FullUserDto par exemple qui va nous afficher tous les détailles comme notre id, notre rôle, etc.

Tous les endpoints fait pour User ont été fait pour les autres entités restantes.

## 04/06/2024
Un fichier seed.sql qui insért quelques données dans la base de données a été fait.

J'ai testé tous les endpoints (pour le moment j'ai que les GET all ou GET by id) pour tous les entités dans Postman. (quelques problèmes bien sûr, rien ne fonctionne du premier coup)

Une collection Postman a aussi été exporté et ajouté dans le projet pour avoir tous les tests nécessaires. La collection va être mis à jour une fois que plusieurs endpoints de l'API vont être créer.

## 05/06/2024
Le POST CreateUser() a été réalisé. Je vais devoir probablement ajouter / supprimer quelques NOT NULL ou autre chose sur la base de données. Pour le moment, je vais faire le CRUD pour avoir une base et comprendre un peu plus comment créer l'API avec ces nouveaux outils et après je vais penser a voir comment je vais faire pour avoir quelque chose plus cohérent et potentiellement mettre en place un système d'authentification avec un token et bla bla bla.

## 06/06/2024
Tous les POST (avec tous les problèmes) ont été fait pour chaque entité.

J'ai réussi a mettre swagger dans le conteneur Docker dans l'API sur swagger/index.html.
Il fonctionner sur IIS Express mais comme j'ai changer assez vite sur Docker, il fonctionnait plus, mais c'est bon maintenant.

## 07/06/2024
PUT endpoint ont été faite pour tous les entités.

J'ai commencé à faire tous les DELETE et j'ai avancé un peu mais j'ai pas encore finir a faire en sorte qu'une fois qu'on supprime un utilisateur, il faut aussi supprimer son compte, ses présences, etc. Par contre, l'action de juste supprimer fonctionne.