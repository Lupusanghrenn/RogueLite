# RogueLite
Rogue Lite en 2D side scroller avec Homère, Léo, Emmanuel et Corentin
 
## Fiche technique
  -Genre : Roguelite<br>
  -2d side scroller<br>
  -Nombre de joueur : 1<br>
## Pitch :
  On reprends la recette des roguelites :<br>
      -génération procédurale : niveau, mobs, items<br>
      -mort = reset<br>
      -à la mort : retour à un hub qui permet d'améliorer son perso<br>
  
## Déroulement d'une partie	
   En début de run, notre équipe est générée procéduralement à la façon des armes dans Borderlands 3 / persos de Rogue Legacy.<br>
    -une BASE qui définie la "classe" du personnage (exemple :ninja, guerrier, archer, etc…)<br>
    -2 préfixes (attributs) qui viennent ajouter des statistiques et spécificités aux personnages. Les traits doivent restés secondaires et ne pas influencer trop fortement la façon de jouer.<br>
    -La base comme les préfixes ont une utilité principale (quand on joue le perso) et secondaire (quand le perso reste dans le vaisseaux)<br>
  On décide du personnage principal ainsi et de son armement, qui va parcourir le niveau. On choisit également un personnage support qui octroie une capacité spéciale active ultime (avec gros cooldown), et un petit passif. Les autres membres d’équipage resteront passif à bord du vaisseau.<br>
  La mort d'un personnage est définitive, mais le personnage drop un ( ou plusieurs) éléments de son stuff.
  On doit envoyer un autre personnage refaire le niveau. Il peut retourner au cadavre de l'ancien personnage et reprendre une partie de son stuff qu'il aura en partie lâché au sol.<br>
  Lors d’une défaite sur un niveau, le vaisseau perd 1 point de vie. La partie s’arrête si le vaisseau n’a plus de vie, peu importe le nombre de membre d’équipage restant.<br>
  Les niveaux sont découpés en tableaux reliés les uns aux autres par des portes (haut bas droite gauche). l’assemblage des différents tableaux est généré procéduralement, mais le contenu/architecture/points de spawn des tableaux est prédéfini. Les ennemis qui apparaissent dans un tableau sont définis procéduralement(au moins un ptit peu).<br>
  Pendant un niveau, un personnage peut monter de niveau /?\ à la fin uniquement? /?\. A ce moment, le joueur se voit proposer un choix entre deux upgrade de son pouvoir actif /?\et passif?/?\ choisies dans un pool défini d’upgrade spécifique à cette classe.<br>
  Lors de sa run, dans les niveaux, il sera possible de récupérer une monnaie permettant d’améliorer de manière permanente ses classes de personnages, mais également de pouvoir reroll ses prochains équipages de départ disponible.<br>
  Tous les personnages possèdent une arme de base commune, qui sera améliorée grâce à des modules trouvés dans les niveaux (cadence de tir, type de missile, …). Ces modules peuvent être transmis à d’autres membres d’équipage pour un prochain niveau.<br>

## Bestiaire :

## Génération procédurale :
  Prefabs de room créés
  Start room
  End room
  Salle Level up
  Mob spawner
  Coffres
  Coffres à clé
  mur de pierre (pétable avec explosifs)

## Player Stats :
  Move speed
  Jump height
  Health
  Jump number
  Damage reduction
  Lifesteal (seulement affecté par les modules)
  Luck
  Crit?
## éléments de décors :
Caisse en bois : peut drop vie, pièce

## Player Inventory :
Le joueur à un inventaire de taille illimité de lequel il stock tous les modules qu’il a. Il est possible de transférer des items d’un perso à l’autre.

##Player Weapon stats :
  Fire rate
  Bullet per shot
  Damage Multiplicator
  Bullet type
## Player class :
  ### Ninja :
    Compétence : Wall jump.
    Upgrades :
      Reset dash après wall jump
      invu pendant le dash
      téléportation à la place du dash
      dash plus long
      réduction de cooldown sur le dash
    Passif : peut redrop une pièce qu’il a ramassé basé sur la luck.
    Ultimate : Dash qui fait des dégâts. Si un ennemi est tué le dash est reset.

  ### Bastion :
    Compétence : Se fixe (aucun mouvement possible) à un endroit et DPS fort avec réduc de dégâts.
    Upgrades :
      réduction de dégâts augmentée.
      munition du joueur utilisée en mode tourelle
      petite explosion lors du changement de forme initial
      random grenade qui spawn autour du joueur
      fait apparaître son drône de combat autour de lui
    Passif : révèle la pos de la salle lvl up.
    Ultimate : drone qui tire sur les ennemis.
    
  ### Shield Dude:(templier ?)
    Compétence : fait apparaître un bouclier sur son arme qui le protège des projectiles.
    Upgrades :
      tirs reflétés
      zone de shield plus grande
      regen de vie passive pendant l’activation de la compétence.
      bloque les ennemis au cac
      dégâts au contact du shield.
    Passif : réduction de dégâts.
    Ultimate : Donne un shield autour du perso pendant une durée.

  ### Wraith:
    Compétence : invulnérable mais ne peut plus taper.
    Upgrades :
      premier tir après phase fait plus de dégâts
      premier tir après la phase traverse les ennemis
      vitesse augmentée en phase
      laisse un clone à l’activation de la phase.
      réactiver le pouvoir dans les cinq secondes téléporte à l’endroit de la première activation
    Passif : move speed passive
    Ultimate : SlowMo d’ennemis

  ### Riposte:
    Compétence : petit lifesteal + dégâts réduits pendant un court temps, et renvoie ensuite en zone les dégâts subis.
    Upgrades : 
      renvoie une partie des projectiles absorbés
      rayon augmenté
      les dégâts renvoyés heal le joueur
      dégâts de compétence augmentés en fonction des PV manquants.
      Si mort pendant la compétence activée active son explosion. si l’explosion tue un ennemi, revive à 50% HP
    Passif : petit lifesteal passif
    Ultimate : dégâts en zone autour de lui fonction des PV manquants, se heal en fonction.

  ### Gunzerker:
    Compétence : augmente sa cadence de tir pendant un temps.
    Upgrades : 
      plus de fire rate
      plus de balles par tir (cône)
      balles rebondissantes
      tire des balles derrière lui
      balles homing
    Passif : fire rate up
    Ultimate : Pluie de balles.

