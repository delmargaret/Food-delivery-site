import React from 'react';
import ImageUploader from 'react-images-upload';
//import './images-uploader.css'

export default class ImagesUploader extends React.Component {
    constructor(props) {
        super(props);
        this.state = { file: null, url: null };
        this.onDrop = this.onDrop.bind(this);
    }

    onDrop(pictureFiles, pictureDataURLs) {
        this.setState({
            file: pictureFiles, url: pictureDataURLs
        });
    }

    render() {
        return (
            <ImageUploader
                withIcon={true}
                withPreview={true}
                buttonText='Выберите картинку'
                label='до 5мб, jpg|png|svg'
                onChange={this.onDrop}
                imgExtension={['.jpg', '.png', ".svg"]}
                fileTypeError="Недопустимое расширение"
                maxFileSize={5242880}
                fileSizeError="Файл слишком большой"
                singleImage={true}
            />
        );
    }
}