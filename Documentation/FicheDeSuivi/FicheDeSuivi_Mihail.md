# Table of Contents
- [**18/05/2024**](#18052024)
- [**23/05/2024**](#23052024)
- [**24/05/2024**](#24052024)

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