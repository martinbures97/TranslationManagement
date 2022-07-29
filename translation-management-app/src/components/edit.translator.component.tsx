import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from "react-router-dom";
import Translator from "../models/translator";
import TranslatorService from "../services/translator.service";
import TranslatorPage from "./page.translator";
import { toast } from "react-toastify"


interface IState {
    translator: Translator
}

function Edit() {
    const { id } = useParams();
    const [values, setValues] = useState({} as IState);
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

    useEffect(() => {
        if(id === "") {
            toast.error("Missing Id parameter. Can't edit translator.");
            navigate("/", { replace: true });
        }
            
        const fetchTranslator = async () => {
            let response = await TranslatorService.get(id!);
            if(response.IsSuccess) {
                setValues({
                    translator: response.Data
                })
            }
            else {
                toast.error("Error while fetching translator from db. " + response.Message);
            }
        }

        fetchTranslator();
    }, []);

    const onSave = async () => {
        let translator = values.translator;
        var response = await TranslatorService.update(id!, translator.name, translator.hourlyRate, translator.creditCardNumber, translator.type!);
        if(response.IsSuccess) {            
            toast.success("Translator successfully updated.");    
        } else {
            toast.error("Error while updating translator." + response.Message);
        }        
    }

    if(values.translator) {
        return (
            <TranslatorPage
                translator={values.translator}
                onChange={onFieldValueChange}
                onSave={onSave}
            />
        );
    }
    else{
        return (
            <div></div>
        );
    }
}

export default Edit;