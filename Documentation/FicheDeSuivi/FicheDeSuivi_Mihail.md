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