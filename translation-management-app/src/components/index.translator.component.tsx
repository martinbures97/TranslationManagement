import { useState, useEffect } from "react";
import TranslatorService from "../services/translator.service";
import { TranslatorType } from "../enums/translator.type.enum";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import ReadonlyTranslatorDto from "../models/readonly.translator";


interface IState {
    listTranslators: Array<ReadonlyTranslatorDto>;
}

function Index () {
    const [translatorList, setTranslatorList] = useState({
        listTranslators: [],
    } as IState);

    useEffect(() => {
        const fetchAndSetData = async () => {
            let response = await TranslatorService.getAll();
            if(response.IsSuccess) {
                setTranslatorList({
                    listTranslators: response.Data
                });
            }
            else {
                toast.error("Error while fetching data from database. " + response.Message);
            }                            
        }

        fetchAndSetData();
    }, []);

    const  deleteTranslator = async (id?: string) => {
        let result = window.confirm("Are your sure you want to delete translator?");
        if (result) {
            let response = await TranslatorService.delete(id!);
            if(response.IsSuccess) {
                setTranslatorList({
                    listTranslators: translatorList.listTranslators.filter(t => t.id !== id)
                });

                toast.success("Translator successfully deleted.");
            }
            else {
                toast.error("Error while deleting translator. " + response.Message);
            }            
        }
    }

    return (
        <div className="mt-3">
            <h3 className="text-center">Translator List</h3>
            <nav>
                <Link className="float-end" to="/create">Create translator</Link>
            </nav>
            <table className="table table-striped" style={{ marginTop: 20 }}>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Hourly rate</th>
                        <th>Credit card number</th>
                        <th>Type</th>
                        <th className="text-center">Assigned jobs</th>
                        <th className="text-center" colSpan={2}>
                            Action
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        translatorList.listTranslators.map((object, i) => {
                            return (<tr key={i}>
                                <td>{object.name}</td>
                                <td>{new Intl.NumberFormat('cs-CZ', {
                                        style: 'currency',
                                        currency: 'CZK',
                                    }).format(object.hourlyRate)}
                                </td>
                                <td>{object.creditCardNumber}</td>
                                <td>{TranslatorType[object.type!]}</td>
                                <td className="text-center">{object.jobIds.length}</td>
                                <td>
                                    <Link to={"/edit/" + object.id} className="btn btn-primary float-end">
                                        Edit
                                    </Link>
                                </td>
                                <td>
                                    <button onClick={() => deleteTranslator(object.id)} className="btn btn-danger">
                                        Delete
                                    </button>
                                </td>
                            </tr>);
                        })
                    }
                </tbody>
            </table>
        </div>
    )
}

export default Index;
