// � Xavalon. All rights reserved.

using System.Xml;
using Xavalon.XamlStyler.Core.MarkupExtensions.Parser;
using Xavalon.XamlStyler.Core.Model;

namespace Xavalon.XamlStyler.Core.MarkupExtensions.Formatter
{
    public class AttributeInfoFactory
    {
        private readonly AttributeOrderRules orderRules;
        private readonly MarkupExtensionParser parser;

        public AttributeInfoFactory(MarkupExtensionParser parser, AttributeOrderRules orderRules)
        {
            this.parser = parser;
            this.orderRules = orderRules;
        }

        public AttributeInfo Create(XmlReader xmlReader)
        {
            string attributeName = xmlReader.Name;
            string attributeValue = xmlReader.Value;
            AttributeOrderRule orderRule = this.orderRules.GetRuleFor(attributeName);
            MarkupExtension markupExtension = this.ParseMarkupExtension(attributeValue);

            return new AttributeInfo(attributeName, attributeValue, orderRule, markupExtension);
        }

        private MarkupExtension ParseMarkupExtension(string value)
        {
            // Only try to parse if there is a chance that it is a markup extension.
            if (value.IndexOf('{') != -1)
            {
                MarkupExtension markupExtension;
                if (this.parser.TryParse(value, out markupExtension))
                {
                    return markupExtension;
                }
            }

            return null;
        }
    }
}