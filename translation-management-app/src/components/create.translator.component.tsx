import React, { useState } from "react";
import Translator from "../models/translator";
import TranslatorService from "../services/translator.service";
import TranslatorPage from "./page.translator";
import { useNavigate } from "react-router-dom"
import { toast } from "react-toastify"


interface IState {
    translator: Translator
}

function Create() {
    const [values, setValues] = useState({
        translator: {
            name: "",
            hourlyRate: 0,
            creditCardNumber: "",
            type: undefined
        }
    } as IState);
    const navigate = useNavigate();

    const onFieldValueChange = (event: React.ChangeEvent<HTMLInputElement>) => {         
        setValues({
            ...values,
            translator: {
                ...values.translator,
                [event.target.name]: event.target.value
            }
        });
    }

    const onSave = async () => { 
        let translator = values.translator;
        let response = await TranslatorService.create(
            translator.name,
            translator.hourlyRate,
            translator.creditCardNumber,
            translator.type!
        );

        if(response.IsSuccess) {
            toast.success("New translator successfully created.");
            navigate("/", { replace: true });
        } else {
            toast.error("Error while creating translator. " + response.Message);
        }            
    } 
     
    return (
        <TranslatorPage
            translator={values.translator}
            onChange={onFieldValueChange}
            onSave={onSave}
        />
    );         
}

export default Create;