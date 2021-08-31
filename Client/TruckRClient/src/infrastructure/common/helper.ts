export class Helper {
    public static toFormData(model:any, form: FormData = null, namespace: string = ""):FormData {
        let formData = form || new FormData();

        for (let propertyName in model) {
            if (!model.hasOwnProperty(propertyName) || !model[propertyName]) continue;
            let formKey = namespace ? `${namespace}[${propertyName}]` : propertyName;
      
            if (model[propertyName] instanceof Date) {
              formData.append(formKey, model[propertyName].toUTCString());
            } else if (
              Array.isArray(model[propertyName]) &&
              model[propertyName].every(item => item instanceof File)
            ) {
              model[propertyName].forEach(file => {
                formData.append(formKey, file);
              });
            } else if (model[propertyName] instanceof Array) {
              model[propertyName].forEach((element, index) => {
                const tempFormKey = `${formKey}[${index}]`;
                this.toFormData(element, formData, tempFormKey);
              });
            } else if (model[propertyName] instanceof File) {
              formData.append(formKey, model[propertyName]);
            } else if (
              typeof model[propertyName] === "object" &&
              !(model[propertyName] instanceof File)
            )
              this.toFormData(model[propertyName], formData, formKey);
            else formData.append(formKey, model[propertyName].toString());
          }

        return formData;
    }

    public static addDays(date: Date, days: number): Date {
      date.setDate(date.getDate() + days);
      return date;
  }
}
