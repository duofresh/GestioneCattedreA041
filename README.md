1. Program.cs:
Questo file rappresenta il punto di ingresso principale dell'applicazione. La classe Program contiene il metodo Main, che esegue i seguenti passi:

Legge i file di risorse (discipline, docenti, classi) dalla cartella res.
Inizializza un'istanza della classe GestioneOrario per gestire i dati delle discipline, docenti e classi.
Inizializza una TriplaNecessita per calcolare le necessità di assegnazione.
Visualizza le discipline, i docenti, le classi e le necessità attraverso metodi di stampa.
Gestisce l'assegnazione dei docenti alle classi utilizzando AssegnaProf.


2. Classe.cs:
Questo file definisce la classe Classe che rappresenta una classe scolastica. Ecco i punti principali:

Ogni classe ha un nome e un numero di studenti.
Viene caricato un elenco di ore di insegnamento per ciascuna disciplina tramite il metodo GetOrePerDisciplina.
La classe contiene la logica per l'assegnazione dei professori alle discipline della classe, bilanciando le ore disponibili dei docenti e le ore richieste dalle materie.
Il metodo AssegnaProfessoriPerClasse è particolarmente interessante: cerca di assegnare i professori in base alle ore disponibili, escludendo eventuali supplenti, e restituisce false se l'assegnazione fallisce.


3. Disciplina.cs:
Questo file definisce la classe Disciplina, che rappresenta una disciplina o materia insegnata. Non avendo accesso completo al contenuto, posso dedurre che la classe probabilmente contiene campi e metodi per rappresentare il nome della disciplina e il numero di ore richieste per ogni classe.


4. Docente.cs:
La classe Docente rappresenta un insegnante. Probabilmente, contiene informazioni come il nome del docente e il numero di ore che può insegnare. Viene utilizzata durante l'assegnazione dei docenti alle classi per verificare se hanno ore sufficienti e se non sono già stati assegnati.


5. GestioneOrario.cs:
Questa classe gestisce l'orario delle lezioni e contiene i seguenti elementi chiave:


Gestisce l'elenco delle discipline, docenti e classi caricando i dati dai file discipline.txt, docenti.txt e classi.txt.
Fornisce metodi per stampare le discipline, i docenti e le classi caricate.
Funziona come un coordinatore tra classi, docenti e discipline per gestire correttamente l'orario.


6. TriplaNecessita.cs:
Questa classe sembra essere responsabile della logica che calcola le necessità di insegnamento per ciascuna combinazione di docenti, discipline e classi. Il metodo StampaNecessita stampa i risultati, mentre il metodo AssegnaProf prova a fare una prima assegnazione dei professori.
