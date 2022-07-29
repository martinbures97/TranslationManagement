import { TranslatorType } from "../enums/translator.type.enum";

export default class Translator {
    id?: string;
    name: string = "";
    hourlyRate: number = 0;
    creditCardNumber: string = "";
    type?: TranslatorType;
}

