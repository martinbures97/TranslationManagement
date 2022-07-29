import axios from "axios";
import Translator from "../models/translator";
import ReadonlyTranslatorDto from "../models/readonly.translator";
import ApiResponse from "../models/api.response";

export default class TranslatorService {
    private static baseURL: string = "http://localhost:5000/api/translators"

    public static async getAll() : Promise<ApiResponse> {
        try {
            var response = await axios.get<Array<ReadonlyTranslatorDto>>(this.baseURL);
            if(response && response.status === 200) {
                return new ApiResponse(true, response.data, "Ok");
            }

            return new ApiResponse(false, undefined, "");
        } catch (error: any) {            
            return this.handleError(error);
        }
    }

    public static async get(id: string): Promise<ApiResponse> {
        try {
            var response = await axios.get<Translator>(this.baseURL + "/" + id);
            if(response && response.status === 200) {
                return new ApiResponse(true, response.data, "Ok");
            }

            return new ApiResponse(false, undefined, "");
        } catch (error: any) {
            return this.handleError(error);
        }
    }
    
    public static async create(name: string, hourlyRate: number, creditCardNumber: string, type: number) : Promise<ApiResponse> {
        try {
            var response = await axios.post(this.baseURL, { name, hourlyRate, creditCardNumber, type: parseInt(type.toString()) });
            if(response && response.status === 200) {
                return new ApiResponse(true, response.data, "Ok");
            }

            return new ApiResponse(false, undefined, "");
        } catch (error: any) {     
            return this.handleError(error);
        }
    }

    public static async update(id: string, name: string, hourlyRate: number, creditCardNumber: string, type: number): Promise<ApiResponse> {
        try {
            var response =  await axios.put(this.baseURL, { id, name, hourlyRate, creditCardNumber, type: parseInt(type.toString()) });
            if(response && response.status === 200) {
                return new ApiResponse(true, response.data, "Ok");
            }

            return new ApiResponse(false, undefined, "");
        } catch (error: any) {            
            return this.handleError(error);
        }       
    }

    public static async delete(id: string): Promise<ApiResponse> {
        try {
            var response =  await axios.delete(this.baseURL, { data: {
                id: id
            } });
            if(response && response.status === 200) {
                return new ApiResponse(true, response.data, "Ok");
            }

            return new ApiResponse(false, undefined, "");
        } catch (error: any) {            
            return this.handleError(error);
        }
    }

    private static handleError (error: any) : ApiResponse {
        if(error.response.data) {
            let data = error.response.data;
            if(data.errors) {
                var errors = Object.keys(data.errors).map(key => {
                    return data.errors[key]
                });
                
                return new ApiResponse(false, undefined, errors.join(". "));
            } else if(data.detail) {
                return new ApiResponse(false, undefined, data.detail);
            }
            else {                
                return new ApiResponse(false, undefined, "Uknown error.");
            }
        } else {
            return new ApiResponse(false, undefined, error.message);
        }
    }
}