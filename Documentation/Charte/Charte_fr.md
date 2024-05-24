# Sommaire
- [**Description**](#description)
- [**Contexte**](#contexte)
- [**Fonctionnalités**](#fonctionnalités)
- [**Diagramme Gantt**](#Diagramme-gantt)
- [**Technologies Utilisées**](#technologies-utilisées)
- [**Critères de succés**](#critères-de-succès)
- [**Rôles et Responsabilités**](#rôles-et-responsabilités)

## Description
Notre projet (YPlanning) a comme but la gestion des cours (inspiré
de YNOV HyperPlanning). C’est une application web qui permet
aux étudiants de consulter les cours et de regarder leurs notes.
L’autre partie du projet sera conservée pour les professeurs qui
pourront mettre les notes des étudiants, faire la présence et
renvoyer le cours aux élèves.

|                |                                                          |
|:---------------|:---------------------------------------------------------|
| Nom de projet  | YPlanning                                                |
| Chef de projet | MIHUTA Mihail                                            |
| Équipe         | MIHUTA Mihail <br> TCHOKOTE HAPPY Joël Christian         |
| GitHub         | [YPlanning](https://github.com/MihutaMihail/YPlanning)   |


## Contexte
- <ins>MIHUTA Mihail</ins> <br>
Ce projet me permet de travailler plus avec C# et PHP, utiliser des
nouveaux outils comme un ORM (Object Relational Mapper) ainsi
que d’intégrer mon code avec l'infrastructure (DevOps).

- <ins>TCHOKOTE HAPPY Joël Christian</ins> <br>
Ce projet me permet de mettre en place une infrastructure
comprenant plusieurs serveurs, un pare-feu, ainsi que de gérer la
configuration et la sécurisation de l’infrastructure qui soutiendra
mon parcours dans la cybersécurité.

## Fonctionnalités
### Authentification
<img src="./img/use_case_authentification.jpg">

### Administrateur
<img src="./img/use_case_admin.jpg">

### Etudiant
<img src="./img/use_case_etudiant.jpg">

### Professeur
<img src="./img/use_case_prof.jpg">

## Diagramme GANTT
- met l'image de diagramme de GANTT
- met l'image de diagramme de GANTT
- met l'image de diagramme de GANTT

## Technologies Utilisées
### Dev
- Visual studio pour IDE
- C# pour le développement de l'API
- ASP.NET Core en tant que framework
- Entity Framework (EF) Core en tant que ORM

### Infra
- Docker pour l'utilisation des serveurs
- PostgreSQL ou MySQL pour le serveur web
- ...

## Critères de succès
Les fonctionnalités à avoir pour que le projet soit considéré comme viable sont :

- *Un système d’authentification*
- *Les étudiants peuvent consulter les cours* 
- *Les étudiants peuvent regarder leurs notes*
- *Les professeurs peuvent faire la présence*
- *Les professeurs peuvent attribuer des notes*
- *Les administrateurs peuvent faire des opérations CRUD (Create, Read, Update, Delete) sur les cours ainsi que sur les étudiants*

## Rôles et responsabilités
**MIHUTA Mihail** --- <ins>*Développeur*</ins>

<ins>Responsabilités</ins>
- Développement de l’API (authentification, opération CRUD, interaction avec base de données)
- Développement du Front (l’interface) pour interagir avec le Backend
- Intégration de la base de données
- Partie dev de la documentation technique
- Préparer le README et la documentation de l'API

---

**TCHOKOTE HAPPY Joël Christian** --- <ins>*Ingénieur en sécurité et infrastructure*</ins>

<ins>Responsabilités</ins>
- Mettre en place l’infrastructure (serveurs, pare-feu, vlans)
- Configuration de l’infrastructure (serveur dns, dhcp, web et base de données)
- Sécurisation de l’infrastructure (gestion des accès (ACL), configuration pare-feu)
- Déploiement de l’API sur le serveur web
- Partie infra de la documentation technique
- UML détaillant la base de données
- Création des schémas pour les usages (use case)