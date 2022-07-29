import Translator from "../models/translator";
import TranslatorForm from "../components/form.translator.component";

interface IProps {
    translator: Translator;
    onChange: React.ChangeEventHandler;
    onSave: () => void;
}

function TranslatorPage (props: IProps) {  
    return (
        <TranslatorForm
            translator={props.translator}
            onChange={props.onChange}
            onSave={props.onSave}
        />
    );
}

export default TranslatorPage;