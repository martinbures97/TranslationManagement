import * as React from "react";
import { Link } from "react-router-dom"
import { Form, Col, Button, ButtonToolbar } from 'react-bootstrap';
import { TranslatorType } from "../enums/translator.type.enum";
import Translator from "../models/translator";
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';

function GetEnumValues() {
    return Object.keys(TranslatorType).reduce((acc: any[], curr, index, arr) => {
        if (index < arr.length / 2) acc.push(curr);
        return acc;
    }, [])
}

interface IProps {
    translator: Translator;
    onChange: React.ChangeEventHandler<HTMLElement>;    
    onSave: () => void;
}

type TranslatorSubmitForm = {
    name: string;
    hourlyRate: number;
    creditCardNumber: number;
    type: number;
  };

function TranslatorForm (props: IProps) {

    const validationSchema = Yup.object().shape({
        name: Yup.string()
          .required('Name is required.')
          .max(50, 'Name must not exceed 50 characters.'),
        hourlyRate: Yup.number()
          .required('Hourly rate is required.')
          .min(1, "Value of hourly rate must be higher than 0."),
        creditCardNumber: Yup.string()
          .required('Credit card number is required.')
          .max(19, "Credit card number must not exceed 19 characters."),
        type: Yup.string()
          .required('Type is required.')        
      });

    const {
        register,
        handleSubmit,        
        formState: { errors }
      } = useForm<TranslatorSubmitForm>({
        resolver: yupResolver(validationSchema)
      });

    const onSubmit = async (data: TranslatorSubmitForm) => {
        await props.onSave();
    };

    return (
        <div>
            <nav className="mt-3">
            <h3 className="text-center">{props.translator.id ? 'Update translator' : 'Add translator'}</h3>
                <Link className="float-end" to="/">Back to translator list</Link>
            </nav>

            <Col md={4} className="justify-content-center mx-auto">
                <Form onSubmit={handleSubmit((e) => onSubmit(e))}>
                    <Form.Group className="mb-3">
                        <Form.Label>Name</Form.Label>
                        <Form.Control {...register('name')} className={` ${errors.name ? 'is-invalid' : ''}`} type="text" onChange={props.onChange} value={props.translator.name} name="name" placeholder="Enter name" />
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Hourly rate</Form.Label>
                        <Form.Control {...register('hourlyRate')} className={` ${errors.hourlyRate ? 'is-invalid' : ''}`} type="number" onChange={props.onChange} value={props.translator.hourlyRate} name="hourlyRate" placeholder="Enter hourly rate" />
                        <Form.Text className="invalid-feedback">{errors.hourlyRate?.message}</Form.Text>
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Credit card number</Form.Label>
                        <Form.Control {...register('creditCardNumber')} className={` ${errors.creditCardNumber ? 'is-invalid' : ''}`} type="number" onChange={props.onChange} value={props.translator.creditCardNumber} name="creditCardNumber" placeholder="Enter credit card number" />
                        <Form.Text className="invalid-feedback">{errors.creditCardNumber?.message}</Form.Text>
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Type</Form.Label>
                        <Form.Select {...register('type')} className={` ${errors.type ? 'is-invalid' : ''}`} name="type" onChange={props.onChange} value={props.translator.type !== null ? props.translator.type : ''}>
                            <option value="" disabled>Select type</option>
                            {GetEnumValues().map(value => (
                                <option key={TranslatorType[value]} value={value}>
                                    {TranslatorType[value]}
                                </option>
                            ))}
                        </Form.Select>
                        <Form.Text className="invalid-feedback">{errors.type?.message}</Form.Text>
                    </Form.Group>

                    <Button 
                        type="submit"
                        className="btn btn-success mt-2 float-end">
                            Save
                    </Button>
                </Form>
            </Col>
        </div>
    )
};

export default TranslatorForm;