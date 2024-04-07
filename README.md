*****Students Management API*****

***Construction details***

Student Management API is a .Net 8 WebApi solution, based on Domain Driven Design, which allows posting students answers to exam questions and retrieving their grades.

The solution is structured into the following projects, where the configuration and dependency injection are orchestrated on StudentsManagement.Web.csproj.

![image](https://github.com/eduferreira1989/StudentsManagement/assets/31478417/197d596f-cbef-47bb-8258-2bc37997dfc1)

***Endpoints***

There are 4 use cases supported by the API:

- GetStudentsByExam: Given the examId the API returns all the students that took the exam.

- GetStudentById: Given the studentId the API returns the student and details the exams, questions and grades for the student (Exam and Question data response is flattened for better readability, Exam grades are not persisted, they are calculated on runtime).

- AddAnswerByExam: Given the examId and the answer payload the API inserts/updates the answer of a student for a given question, from an exam he/she took.

- GetAnswerById: Given the answerId the API returns the details of the answer.

The endpoints can be tested through Swagger which is launched when the application runs:

![image](https://github.com/eduferreira1989/StudentsManagement/assets/31478417/3359a5db-dbb9-4c9c-b5b1-815841d2eb74)

***Database***

The database is faked by an InMemory database provided by EntityFramework that simulates a real database while the application runs. DB Schema is defined below:

![image](https://github.com/eduferreira1989/StudentsManagement/assets/31478417/a7897400-086c-44ea-8267-05efb7175664)

***Data***

For testing purpose the InMemory database is populated with the following data:

- 5 students:

  1: Eduardo Ferreira
    
	2: Lewis Hamilton
    
	3: Max Verstappen

  4: Simone Simons
   
	5: Floor Jansen

- 3 exams:

	1: C# Exam
  
	2: Python Exam
  
	3: Greeting Exam
  
- 6 questions:
  
	3 questions for exam 1, with 1 right answer each
 	
	2 questions for exam 2, with 1 right answer each
 	
	1 question for exam 3, with 3 right answers
 	
- 7 relations Student X Exam:
  
	Student 1 (Eduardo Ferreira) took exams 1 and 2

	Student 2 (Lewis Hamilton) took exam 2

	Student 3 (Max Verstappen) took exam 1

	Student 4 (Simone SImons) took exam 3

	Student 5 (Floor Jansen) took exam 1 and 3

***Test suggestion***

Adding answers:

- Produces right answer -> Grade 10 for the exam

- ExamId: 3

		{
		  "id": 1,
		  "questionId": 6,
		  "studentId": 4,
		  "answerText": "sAlUt"
		}

________________________________________________________________________________________

- Produces wrong answer -> Grade 0 for the exam

- ExamId: 3

		{
		  "id": 1,
		  "questionId": 6,
		  "studentId": 4,
		  "answerText": "SalutZ"
		}

________________________________________________________________________________________

- Produces 2 right answers and 1 wrong answer -> Grade 7 for the exam

- ExamId: 1

		{
		  "id": 2,
		  "questionId": 1,
		  "studentId": 1,
		  "answerText": "It is a plataform for software development multiplatform and multipurpose."
		}

- Then

		{
		  "id": 3,
		  "questionId": 2,
		  "studentId": 1,
		  "answerText": "It is a programming language developed, it is the most used language in .Net ecosystem."
		}

- Then

		{
		  "id": 4,
		  "questionId": 3,
		  "studentId": 1,
		  "answerText": "The current version is .Net 6, tagged as Long Term Support."
		}

________________________________________________________________________________________

- Produces 2 right answers -> Grade 10 for the exam

- ExamId: 2

		{
		  "id": 5,
		  "questionId": 4,
		  "studentId": 1,
		  "answerText": "It is a language with fast learning curve and there are several libraries and platforms developed for this purpose using Python."
		}
  
- Then

		{
		  "id": 6,
		  "questionId": 5,
		  "studentId": 1,
		  "answerText": "It is a casing style generally used when writing Python codes, the names are in lower case splitted by '_', like: 'snake_case'."
		}

________________________________________________________________________________________

The grades can be checked when hitting GetStudentById endpoint, that will return a detailed view of Exams and questions the Student took:

		https://localhost:7022/Student/1
