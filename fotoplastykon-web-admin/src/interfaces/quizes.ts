export interface QuizListItem
{
    id: number;
    name: string;
    photoUrl: string;
}


export interface QuizForm
{
    id: number;
    name: string;
    photoUrl: string;
    questions: QuestionForm[];
}

export interface QuestionForm
{
    id: number;
    questionText: string;
    isMultichoice: boolean;
    answers: AnswerForm[];
}

export interface AnswerForm
{
    id: number;
    answerText: string;
    isCorrect: boolean;
}
