export interface Quiz
{
    id: number;
    name: string;
    questions: Question[];
}

export interface Question
{
    id: number;
    questionText: string;
    isMultichoice: boolean;
    answers: Answer[];
}

export interface Answer
{
    id: number;
    answerText: string;
    isSelected: boolean;
}

export interface QuizResult
{
    id: number;
    name: string;
    points: number;
    questions: QuestionResult[];
}

export interface QuestionResult
{
    id: number;
    questionText: string;
    answers: AnswerResult[];
}

export interface AnswerResult
{
    id: number;
    answerText: string;
    isSelected: boolean;
    isCorrect: boolean;
}

export interface QuizListItem
{
    id: number;
    name: string;
}

export enum QuizState
{
    Starting,
    AnsweringQuestions,
    ShowingResults
}
