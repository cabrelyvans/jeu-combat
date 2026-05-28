# Jeu de Combat Tour par Tour (CLI)

Projet réalisé dans le cadre du TP de Clean Code et Design Patterns (M2 - Sup de Vinci).

## 🚀 Fonctionnalités

- Jeu en console fluide au tour par tour.
- Saisie sécurisée des entrées utilisateur (gestion des erreurs de frappe).
- 3 classes de héros uniques :
  - **Guerrier** (120 PV, 18 ATQ, Compétence : _Frappe lourde_ avec temps de recharge).
  - **Mage** (80 PV, 12 ATQ, Compétence : _Éclair_ magique).
  - **Voleur** (90 PV, 14 ATQ, Compétence : _Coup critique_ avec 30% de chance d'infliger double dégâts).
- Système de gestion de vagues progressives :
  - **Vague 1** : 1 ennemi faible.
  - **Vague 2** : 2 ennemis simultanés.
  - **Vague 3** : Le Boss de fin (_Gromash le Destructeur_).
- Restauration partielle des PV du héros (+20%) lors des transitions entre deux vagues.

## 🏗️ Architecture & Découpage des Projets

Le projet applique rigoureusement les principes de la **Clean Architecture** à travers un découpage en 4 couches distinctes, garantissant l'indépendance de la logique métier vis-à-vis des détails technologiques (IHM Console) :

1. **CombatTourParTour.Domain** : Contient le cœur du métier (les entités pures `Personnage`, `Heros`, `Ennemi` et les énumérations). Aucun élément extérieur n'y est injecté.
2. **CombatTourParTour.Application** : Contient les règles d'exécution du combat, le orchestrateur de la machine à états (`CombatEngine`), les contrats d'interfaces et les classes de Design Patterns.
3. **CombatTourParTour.Infrastructure** : Implémente les détails techniques nécessaires à l'IHM. C'est ici que réside le service de rendu console (`ConsoleCombatUiService`) via l'inversion de dépendances.
4. **CombatTourParTour.Cli** : Point d'entrée de l'application (`Program.cs`). Il fait office de racine de composition (Composition Root) pour instancier les dépendances et démarrer le jeu.

![Schéma d'architecture du projet](image/CombatEngineArchitecture-2026-05-28-121852.png)

## 🛠️ Design Patterns Implémentés

Pour répondre aux contraintes du TP et éviter l'usage de structures conditionnelles complexes (`if/else` ou `switch` imbriqués), 4 patterns de conception ont été combinés :

| Pattern      | Classe(s) / interface(s)                                                                                          | Rôle dans le jeu                                                                                                                                                         |
| :----------- | :---------------------------------------------------------------------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Factory**  | `IHeroFactory`, `HeroFactory`<br>`IEnnemiFactory`, `EnnemiFactory`                                                | Isole la logique de création et le paramétrage des statistiques initiales. Évite la dispersion de mots-clés `new` et de "nombres magiques" dans l'application.           |
| **Strategy** | `ICombatAction`<br>`AttaqueBasiqueAction`<br>`SoinAction`<br>`CompetenceGuerrierAction`...                        | Encapsule les algorithmes d'attaque ou de soin de manière polymorphique. Le moteur applique une action sans savoir s'il s'agit d'un sort magique ou d'une potion.        |
| **Command**  | `ICommand`<br>`AttaquerCommand`<br>`SoignerCommand`<br>`UtiliserCompetenceCommand`                                | Encapsule l'intention d'action du joueur depuis le menu d'IHM. Elle permet d'isoler complètement la saisie clavier (choix de l'index d'une cible) de l'exécution métier. |
| **State**    | `ICombatState`<br>`TourJoueurState`<br>`TourEnnemiState`<br>`EntreVaguesState`<br>`VictoireState`, `DefaiteState` | Gère le flux global de la partie sous forme de machine à états. Chaque état sait comment s'exécuter et désigne dynamiquement l'état suivant au `CombatEngine`.           |

## 🌟 Principes Clean Code Respectés

- **S** (_Single Responsibility Principle_) : Chaque classe possède une responsabilité unique. Par exemple, la classe `Heros` gère ses points de vie, `TourJoueurState` ordonne le déroulement du tour, et `ConsoleCombatUiService` s'occupe exclusivement de l'affichage textuel.
- **O** (_Open/Closed Principle_) : L'application est ouverte à l'extension mais fermée à la modification. Ajouter un nouvel ennemi ou un nouveau sort nécessite simplement de créer une nouvelle classe implémentant `ICombatAction` ou de modifier la `Factory`, sans altérer le moteur de jeu central.
- **D** (_Dependency Inversion Principle_) : La logique applicative dépend uniquement d'abstractions (`ICombatUiService`, `ICommand`, etc.). L'IHM Console est une implémentation interchangeable située à la périphérie.

## ⚙️ Comment lancer le projet

Assurez-vous d'avoir installé le SDK .NET (version 8.0 ou supérieure). À la racine du projet, exécutez la commande suivante :

```bash
dotnet run --project src/CombatTourParTour.Cli
```
